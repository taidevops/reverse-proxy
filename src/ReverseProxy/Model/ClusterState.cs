// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Concurrent;
using Yarp.ReverseProxy.Utilities;

namespace Yarp.ReverseProxy.Model
{
    /// <summary>
    /// Representation of a cluster for use at runtime.
    /// </summary>
    public sealed class ClusterState
    {
        /// <summary>
        /// Creates a new instance. This constructor is for tests and infrastructure, this type is normally constructed by the configuration
        /// loading infrastructure.
        /// </summary>
        public ClusterState(string clusterId)
        {
            ClusterId = clusterId ?? throw new ArgumentNullException(nameof(clusterId));
        }

        /// <summary>
        /// The cluster's unique id.
        /// </summary>
        public string ClusterId { get; }
    }
}
