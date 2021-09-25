// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Yarp.ReverseProxy.Configuration
{
    /// <summary>
    /// Represents a snapshot of proxy configuration data.
    /// </summary>
    public interface IProxyConfig
    {
        /// <summary>
        /// A notification that triggers when this snapshot expires.
        /// </summary>
        IChangeToken ChangeToken { get; }
    }
}
