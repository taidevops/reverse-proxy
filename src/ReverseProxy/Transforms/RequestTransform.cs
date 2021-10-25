// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Utilities;

namespace Yarp.ReverseProxy.Transforms
{
    /// <summary>
    /// The base class for request transforms.
    /// </summary>
    public abstract class RequestTransform
    {
        public abstract ValueTask ApplyAsync(RequestTransformContext context);

        /// <summary>
        /// Removes and returns the current header value by first checking the HttpRequestMessage,
        /// then the HttpContent, and failing back to the HttpContext only if
        /// <see cref="RequestTransformContext.HeadersCopied"/> is not set.
        /// This ordering allows multiple transforms to mutate the same header.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static StringValues TakeHeader(RequestTransformContext context, string headerName)
        {
            if (string.IsNullOrEmpty(headerName))
            {
                throw new System.ArgumentException($"'{nameof(headerName)}' cannot be null or empty.", nameof(headerName));
            }

            var existingValues = StringValues.Empty;
            if (context.ProxyRequest.Headers.TryGetValues(headerName, out var values))
            {
                context.ProxyRequest.Headers.Remove(headerName);
                existingValues = (string[])values;
            }
            else if (context.ProxyRequest.Content?.Headers.TryGetValues(headerName, out values) ?? false)
            {
                context.ProxyRequest.Content.Headers.Remove(headerName);
                existingValues = (string[])values!;
            }
            else if (!context.HeadersCopied)
            {
                existingValues = context.HttpContext.Request.Headers[headerName];
            }

            return existingValues;
        }

        public static void AddHeader(RequestTransformContext context, string headerName)
        {
            if (context is null)
            {
                throw new System.ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(headerName))
            {
                throw new System.ArgumentException($"'{nameof(headerName)}' cannot be null or empty.", nameof(headerName));
            }
        }
    }
}
