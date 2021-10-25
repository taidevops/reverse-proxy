// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.ReverseProxy.Routing
{
    internal sealed class HeaderMatcherPolicy : MatcherPolicy, IEndpointComparerPolicy, IEndpointSelectorPolicy
    {
        /// <inheritdoc/>
        // Run after HttpMethodMatcherPolicy (-1000) and HostMatcherPolicy (-100), but before default (0)
        public override int Order => -50;

        public IComparer<Endpoint> Comparer => new HeaderMetadataEndpointComparer();

        /// <inheritdoc/>
        bool IEndpointSelectorPolicy.AppliesToEndpoints(IReadOnlyList<Endpoint> endpoints)
        {
            _ = endpoints ?? throw new ArgumentNullException(nameof(endpoints));

            // When the node contains dynamic endpoints we can't make any assumptions.
            if (ContainsDynamicEndpoints(endpoints))
            {
                return true;
            }

            return AppliesToEndpointsCore(endpoints);
        }

        private static bool AppliesToEndpointsCore(IReadOnlyList<Endpoint> endpoints)
        {
            return endpoints.Any(e =>
            {
                var metadata = e.Metadata.GetMetadata<IHeaderMetadata>();
                return metadata?.Matchers?.Count > 0;
            });
        }

        public Task ApplyAsync(HttpContext httpContext, CandidateSet candidates)
        {
            _ = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _ = candidates ?? throw new ArgumentNullException(nameof(candidates));

            for (var i = 0; i < candidates.Count; i++)
            {
                if (!candidates.IsValidCandidate(i))
                {
                    continue;
                }

                var matchers = candidates[i].Endpoint.Metadata.GetMetadata<IHeaderMetadata>()?.Matchers;

                if (matchers == null)
                {
                    continue;
                }


            }

            return Task.CompletedTask;
        }

        private class HeaderMetadataEndpointComparer : EndpointMetadataComparer<IHeaderMetadata>
        {
            protected override int CompareMetadata(IHeaderMetadata? x, IHeaderMetadata? y)
            {
                var xCount = x?.Matchers?.Count ?? 0;
                var yCount = y?.Matchers?.Count ?? 0;

                if (xCount > yCount)
                {
                    // x is more specific
                    return -1;
                }
                if (yCount > xCount)
                {
                    // y is more specific
                    return 1;
                }

                return 0;
            }
        }
    }
}
