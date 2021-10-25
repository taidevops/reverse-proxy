// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Yarp.ReverseProxy.Model;

namespace Yarp.ReverseProxy.Health
{
    /// <summary>
    /// Updates destinations' health states when it's requested by a health check policy
    /// while taking into account not only the new evaluated value but also the overall current cluster's health state.
    /// </summary>
    public interface IDestinationHealthUpdater
    {
        void SetPassive(ClusterState cluster, Des)
    }
}
