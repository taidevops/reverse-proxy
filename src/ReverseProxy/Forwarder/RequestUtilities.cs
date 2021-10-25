// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Yarp.ReverseProxy.Forwarder
{
    /// <summary>
    /// APIs that can be used when transforming requests.
    /// </summary>
    public static class RequestUtilities
    {
        internal static void AddHeader(StringValues value)
        {
            if (value.Count == 1)
            {
                string headerValue = value;

                if (ContainsNewLines(headerValue))
                {
                    // TODO: Log
                    return;
                }

                return;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static bool ContainsNewLines(string value) => value.AsSpan().IndexOfAny('\r', '\n') >= 0;
        }
    }
}
