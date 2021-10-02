// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Net.Http;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.ReverseProxy.Model
{
    public sealed class ClusterModel
    {
        /// <summary>
        /// Creates a new Instance.
        /// </summary>
        public ClusterModel(
            ClusterConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// The config for this cluster.
        /// </summary>
        public ClusterConfig Config { get; }

        internal bool HasConfigChanged(ClusterModel newModel)
        {
            return !Config.EqualsExcludingDestinations(newModel.Config);
        }
    }
}
