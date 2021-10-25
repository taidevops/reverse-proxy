// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Yarp.ReverseProxy.Forwarder
{
    /// <summary>
    /// Forward an HTTP request to a chosen destination.
    /// </summary>
    public interface IHttpForwarder
    {
        ValueTask<ForwarderError> SendAsync(HttpContent context, string destinationPrefix, HttpMessageInvoker httpClient,
            ForwarderRequestConfig requestConfig);
    }
}
