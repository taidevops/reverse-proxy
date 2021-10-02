// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Management;
using Yarp.ReverseProxy.Model;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for <see cref="IEndpointRouteBuilder"/>
    /// used to add Reverse Proxy to the ASP .NET Core request pipeline.
    /// </summary>
    public static class ReverseProxyIEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Adds Reverse Proxy routes to the route table using the default processing pipeline.
        /// </summary>
        public static ReverseProxyConventionBuilder MapReverseProxy(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapReverseProxy(app =>
            {

            });
        }

        /// <summary>
        /// Adds Reverse Proxy routes to the route table with the customized processing pipeline. The pipeline includes
        /// by default the initialization step and the final proxy step, but not LoadBalancingMiddleware or other intermediate components.
        /// </summary>
        public static ReverseProxyConventionBuilder MapReverseProxy(this IEndpointRouteBuilder endpoints, Action<IReverseProxyApplicationBuilder> configureApp)
        {
            if (endpoints is null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }
            if (configureApp is null)
            {
                throw new ArgumentNullException(nameof(configureApp));
            }

            var proxyAppBuilder = new ReverseProxyApplicationBuilder(endpoints.CreateApplicationBuilder());
            configureApp(proxyAppBuilder);
            var app = proxyAppBuilder.Build();

            return GetOrCreateDataSource(endpoints).DefaultBuilder;
        }

        private static ProxyConfigManager GetOrCreateDataSource(IEndpointRouteBuilder endpoints)
        {
            var dataSource = endpoints.DataSources.OfType<ProxyConfigManager>().FirstOrDefault();
            if (dataSource == null)
            {
                dataSource = endpoints.ServiceProvider.GetRequiredService<ProxyConfigManager>();
                endpoints.DataSources.Add(dataSource);

                // Config validation is async but startup is sync. We want this to block so that A) any validation errors can prevent
                // the app from starting, and B) so that all the config is ready before the server starts accepting requests.
                // Reloads will be async.
                dataSource.InitialLoadAsync().GetAwaiter().GetResult();
            }

            return dataSource;
        }
    }
}
