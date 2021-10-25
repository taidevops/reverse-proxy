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
        private volatile ClusterModel _model = default!; // Initialized right after construction.
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

        /// <summary>
        /// Encapsulates parts of a cluster that can change atomically in reaction to config changes.
        /// </summary>
        public ClusterModel Model
        {
            get => _model;
            internal set => _model = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Tracks changes to the cluster configuration for use with rebuilding dependent endpoints.
        /// </summary>
        internal int Revision { get; set; }
    }
}
