// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Kubernetes.Fakes;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Microsoft.Kubernetes.Controller.Rate;

public class LimiterTests
{
    [Fact]
    public void FirstTokenIsAvailable()
    {
        var clock = new FakeSystemClock();
        var limiter = new Limiter(new Limit(10), 1, clock);

        var allowed = limiter.Allow();

        Assert.True(allowed);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(1)]
    [InlineData(300)]
    public void AsManyAsBurstTokensAreAvailableRightAway(int burst)
    {
        var clock = new FakeSystemClock();
        var limiter = new Limiter(new Limit(10), burst, clock);

        var allowed = new List<bool>();
        foreach (var index in Enumerable.Range(1, burst))
        {
            allowed.Add(limiter.Allow());
        }
        var notAllowed = limiter.Allow();

        Assert.All(allowed, item => Assert.True(item));
        Assert.False(notAllowed);
    }
}
