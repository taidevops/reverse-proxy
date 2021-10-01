using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Xunit;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Model;

namespace Microsoft.AspNetCore.Builder.Tests
{
    public class ReverseProxyConventionBuilderTests
    {
        [Fact]
        public void ReverseProxyConventionBuilder_Configure_Works()
        {
            var configured = false;

            var conventions = new List<Action<EndpointBuilder>>();
            var builder = new ReverseProxyConventionBuilder(conventions);

            builder.ConfigureEndpoints(builder =>
            {
                configured = true;
            });

            var routeConfig = new RouteConfig();
            var endpointBuilder = CreateEndpointBuilder(routeConfig);

            var action = Assert.Single(conventions);
            action(endpointBuilder);

            Assert.True(configured);
        }

        public void ReserveProxyConventionBuilder_ConfigureWithProxy_Works()
        {

        }

        private static RouteEndpointBuilder CreateEndpointBuilder(RouteConfig routeConfig)
        {
            var endpointBuilder = new RouteEndpointBuilder(context => Task.CompletedTask, RoutePatternFactory.Parse(""), 0);
            var routeModel = new RouteModel(
                routeConfig);

            endpointBuilder.Metadata.Add(routeModel);

            return endpointBuilder;
        }
    }
}
