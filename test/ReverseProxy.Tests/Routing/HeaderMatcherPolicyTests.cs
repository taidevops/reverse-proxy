// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.AspNetCore.Routing.Patterns;
using Xunit;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.ReverseProxy.Routing.Tests
{
    public class HeaderMatcherPolicyTests
    {
        [Fact]
        public void Comparer_SortOrder_SingleRuleEqual()
        {
            // Most specific to least
            var endpoints = new[]
            {
                (0, CreateEndpoint("header", new[] { "abc" }, HeaderMatchMode.ExactHeader, isCaseSensitive: true)),
            };
        }

        private static Endpoint CreateEndpoint(
            string headerName,
            string[] headerValues,
            HeaderMatchMode mode = HeaderMatchMode.ExactHeader,
            bool isCaseSensitive = false,
            bool isDynamic = false)
        {
            return CreateEndpoint(new[] { new HeaderMatcher(headerName, headerValues, mode, isCaseSensitive) }, isDynamic);
        }

        private static Endpoint CreateEndpoint(IReadOnlyList<HeaderMatcher> matchers, bool isDynamic = false)
        {
            var builder = new RouteEndpointBuilder(_ => Task.CompletedTask, RoutePatternFactory.Parse("/"), 0);
            builder.Metadata.Add(new HeaderMetadata(matchers));
            if (isDynamic)
            {
                builder.Metadata.Add(new DynamicEndpointMetadata());
            }

            return builder.Build();
        }

        private class DynamicEndpointMetadata : IDynamicEndpointMetadata
        {
            public bool IsDynamic => true;
        }
    }
}
