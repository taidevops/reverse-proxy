// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.ReverseProxy.Model
{
    /// <summary>
    /// Immutable representation of the portions of a route
    /// that only change in reaction to configuration changes.
    /// </summary>
    /// <remarks>
    /// All members must remain immutable to avoid thread safety issues.
    /// Instead, instances of <see cref="RouteModel"/> are replaced
    /// in their entirety when values need to change.
    /// </remarks>
    public sealed class RouteModel
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RouteModel(
            RouteConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// The configuration data used to build this route.
        /// </summary>
        public RouteConfig Config { get; }
    }
}
