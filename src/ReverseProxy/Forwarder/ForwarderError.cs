// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Yarp.ReverseProxy.Forwarder
{
    /// <summary>
    /// Errors reported when forwarding a request to the destination.
    /// </summary>
    public enum ForwarderError : int
    {
        /// <summary>
        /// No error.
        /// </summary>
        None,

        /// <summary>
        /// Failed to connect, send the request headers, or receive the response headers.
        /// </summary>
        Request,

        /// <summary>
        /// Timed out when trying to connect, send the request headers, or receive the response headers.
        /// </summary>
        RequestTimedOut,

        /// <summary>
        /// Canceled when trying to connect, send the request headers, or receive the response headers.
        /// </summary>
        RequestCanceled,

        /// <summary>
        /// Canceled while copying the request body.
        /// </summary>
        RequestBodyCanceled,

        RequestBodyClient,

        RequestBodyDestination,

        ResponseHeaders,

        ResponseBodyCanceled,

        ResponseBodyClient,

        ResponseBodyDestination,


    }
}
