// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Configuration.ConfigProvider;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InMemoryConfigProviderExtensions
    {
        public static IReverseProxyBuilder LoadFromMemory(this IReverseProxyBuilder builder)
        {
            builder.Services.AddSingleton<IProxyConfigProvider>(new InMemoryConfigProvider());
            return builder;
        }
    }
}

namespace Yarp.ReverseProxy.Configuration.ConfigProvider
{
    public class InMemoryConfigProvider : IProxyConfigProvider
    {
        private volatile InMemoryConfig _config;

        public InMemoryConfigProvider()
        {
            _config = new InMemoryConfig();
        }

        public IProxyConfig GetConfig() => _config;

        public void Update()
        {
            _config = new InMemoryConfig();
        }

        private class InMemoryConfig : IProxyConfig
        {
            private readonly CancellationTokenSource _cts = new CancellationTokenSource();

            public InMemoryConfig()
            {
                ChangeToken = new CancellationChangeToken(_cts.Token);
            }

            public IChangeToken ChangeToken { get; set; }
        }
    }
}
