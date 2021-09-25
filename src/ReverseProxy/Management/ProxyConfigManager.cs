// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.ReverseProxy.Management
{
    /// <summary>
    /// Provides a method to apply Proxy configuraiton changes
    /// by leveraging <see cref="IDynamicEndpointMetadata"/>
    /// </summary>
    internal sealed class ProxyConfigManager : EndpointDataSource, IDisposable
    {
        private readonly object _syncRoot = new object();
        private readonly IProxyConfigProvider _provider;
        private IDisposable? _changeSubscription;

        private List<Endpoint>? _endpoints;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private IChangeToken _changeToken;

        public ProxyConfigManager(
            IProxyConfigProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _changeToken = new CancellationChangeToken(_cancellationTokenSource.Token);
        }

        /// <inheritdoc/>
        public override IReadOnlyList<Endpoint> Endpoints
        {
            get
            {
                // The Endpoints needs to be lazy the first time to give a chance to ReverseProxyConventionBuilder to add its conventions.
                // Endpoints are accessed by routing on the first request.
                if (_endpoints == null)
                {
                    lock (_syncRoot)
                    {
                        if (_endpoints == null)
                        {
                            CreateEndpoints();
                        }
                    }
                }
                _endpoints = new List<Endpoint>();
                return _endpoints;
            }
        }

        private void CreateEndpoints()
        {
            var endpoints = new List<Endpoint>();
            // Directly enumerable the ConcurrentDictionary to limit locking and copying.
            

            UpdateEndpoints(endpoints);
        }

        /// <inheritdoc/>
        public override IChangeToken GetChangeToken() => Volatile.Read(ref _changeToken);

        public async Task<EndpointDataSource> InitialLoadAsync()
        {
            // Trigger the first load immediately and throw if it fails
            // We intend this to crash the app so we don't try listening for future changes.
            try
            {
                var config = _provider.GetConfig();
                await Task.CompletedTask;

                if (config.ChangeToken.ActiveChangeCallbacks)
                {
                    _changeSubscription = config.ChangeToken.RegisterChangeCallback(ReloadConfig, this);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to load or apply the proxy configuration.", ex);
            }

            return this;
        }

        private static void ReloadConfig(object state)
        {
            var manager = (ProxyConfigManager)state;
        }

        private void UpdateEndpoints(List<Endpoint> endpoints)
        {
            if (endpoints == null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            lock (_syncRoot)
            {
                Volatile.Write(ref _endpoints, endpoints);

                // Step 3 - create new change token
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        public void Dispose()
        {
            _changeSubscription?.Dispose();
        }
    }
}
