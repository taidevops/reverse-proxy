// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Yarp.ReverseProxy.Forwarder
{
    public class HttpTransformer
    {
        /// <summary>
        /// A default set of transforms that adds X-Forwarded-* headers, removes the original Host value and
        /// copies all other request and response fields and headers, except for some protocol specific values.
        /// </summary>
        public static readonly HttpTransformer Default;
    }
}
