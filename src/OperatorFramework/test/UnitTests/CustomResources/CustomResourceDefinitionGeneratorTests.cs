// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using k8s.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Kubernetes.CustomResources;

public class CustomResourceDefinitionGeneratorTests
{
    [Fact]
    public async Task MetadataNameComesFromPluralNameAndGroup()
    {
        var generator = new CustomResourceDefinitionGenerator();

        var crd = await generator.GenerateCustomResourceDefinitionAsync<SimpleResource>("Namespaced");

        Assert.Equal("testkinds.test-group", crd.Name());
    }

    [Fact]
    public async Task ApiVersionAndKindAreCorrect()
    {
        var generator = new CustomResourceDefinitionGenerator();

        var crd = await generator.GenerateCustomResourceDefinitionAsync<SimpleResource>("Namespaced");

        Assert.Equal("v1", crd.ApiGroupVersion());
        Assert.Equal(V1CustomResourceDefinition.KubeApiVersion, crd.ApiGroupVersion());
        Assert.Equal("apiextensions.k8s.io", crd.ApiGroup());
        Assert.Equal(V1CustomResourceDefinition.KubeGroup, crd.ApiGroup());
        Assert.Equal("CustomResourceDefinition", crd.Kind);
        Assert.Equal(V1CustomResourceDefinition.KubeKind, crd.Kind);
        crd.Validate();
    }

    [Theory]
    [InlineData("Namespaced")]
    [InlineData("Cluster")]
    public async Task ScopeProvidedByGenerateParameter(string scope)
    {
        var generator = new CustomResourceDefinitionGenerator();

        var crd = await generator.GenerateCustomResourceDefinitionAsync<SimpleResource>(scope);

        Assert.Equal(scope, crd.Spec.Scope);
    }


}
