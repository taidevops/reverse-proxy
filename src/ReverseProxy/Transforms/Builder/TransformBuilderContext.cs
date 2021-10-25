// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.ReverseProxy.Transforms.Builder
{
    /// <summary>
    /// State used when building transforms for the given route.
    /// </summary>
    public class TransformBuilderContext
    {
        /// <summary>
        /// Application services that can be used to construct transforms.
        /// </summary>
        public IServiceProvider Services { get; init; } = default!;

        /// <summary>
        /// The route these transforms will be associated with.
        /// </summary>
        public RouteConfig Route { get; init; } = default!;

        /// <summary>
        /// The cluster config used by the route.
        /// This may be null if the route is not currently paired with a cluster.
        /// </summary>
        public ClusterConfig? Cluster { get; init; }

        /// <summary>
        /// Indicates if request headers should all be copied to the proxy request before transforms are applied.
        /// </summary>
        public bool? CopyRequestHeaders { get; set; }

        /// <summary>
        /// Indicates if response headers should all be copied to the client response before transforms are applied.
        /// </summary>
        public bool? CopyResponseHeaders { get; set; }


    }
}
