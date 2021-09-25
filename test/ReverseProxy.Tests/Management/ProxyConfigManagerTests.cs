// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace Yarp.ReverseProxy.Management.Tests
{
    public class ProxyConfigManagerTests
    {
        private IServiceProvider CreateServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddRouting();
            var proxyBuilder = serviceCollection.AddReverseProxy().LoadFromMemory();
            var services = serviceCollection.BuildServiceProvider();
            return services;
        }

        [Fact]
        public void Constructor_Works()
        {
            var service = CreateServices();
            _ = service.GetRequiredService<ProxyConfigManager>();
        }
    }
}
