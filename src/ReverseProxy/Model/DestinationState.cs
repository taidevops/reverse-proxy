// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using Yarp.ReverseProxy.Utilities;

namespace Yarp.ReverseProxy.Model
{
    /// <summary>
    /// Representation of a cluster's destination for use at runtime.
    /// </summary>
    public sealed class DestinationState : IReadOnlyList<DestinationState>
    {
        private volatile DestinationModel _model = default!;

        /// <summary>
        /// The destination's unique id.
        /// </summary>
        public string DestinationId { get; }

        /// <summary>
        /// A snapshot of the current configuration
        /// </summary>
        public DestinationModel Model
        {
            get => _model;
            internal set => _model = value ?? throw new ArgumentNullException(nameof(value));
        }


    }
}
