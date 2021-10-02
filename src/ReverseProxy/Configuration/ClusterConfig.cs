// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Yarp.ReverseProxy.Utilities;

namespace Yarp.ReverseProxy.Configuration
{
    /// <summary>
    /// A cluster is a group of equivalent endpoints and associated policies.
    /// </summary>
    public sealed record ClusterConfig
    {
        /// <summary>
        /// The Id for this cluster. This needs to be globally unique.
        /// This field is required.
        /// </summary>
        public string ClusterId { get; init; } = default!;

        /// <summary>
        /// Load balancing policy.
        /// </summary>
        public string? LoadBalancingPolicy { get; init; }

        public bool Equals(ClusterConfig? other)
        {
            if (other == null)
            {
                return false;
            }

            return EqualsExcludingDestinations(other);
        }

        internal bool EqualsExcludingDestinations(ClusterConfig other)
        {
            if (other == null)
            {
                return false;
            }

            return string.Equals(ClusterId, other.ClusterId, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                ClusterId?.GetHashCode(StringComparison.OrdinalIgnoreCase));
        }
    }
}
