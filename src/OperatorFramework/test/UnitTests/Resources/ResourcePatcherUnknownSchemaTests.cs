// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Kubernetes.Resources.Models;
using Microsoft.Kubernetes.Utils;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Kubernetes.Resources;

public class ResourcePatcherUnknownSchemaTests : ResourcePatcherTestsBase
{
    [Fact]
    public async Task ObjectPropertyIsAddedWhenMissing()
    {
        await RunStandardTest(TestYaml.LoadFromEmbeddedStream<StandardTestYaml>());
    }
}
