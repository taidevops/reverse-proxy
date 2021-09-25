# Releasing YARP

This document provides a guide on how to release a preview of YARP.

To keep track of the process, open a [release checklist issue](https://github.com/4201104140/reverse-proxy/issues/new?title=Preview%20X%20release%20checklist&body=See%20%5BRelease.md%5D%28https%3A%2F%2Fgithub.com%2Fmicrosoft%2Freverse-proxy%2Fblob%2Fmain%2Fdocs%2Foperations%2FRelease.md%29%20for%20detailed%20instructions.%0A%0A-%20%5B%20%5D%20Ensure%20there%27s%20a%20release%20branch%20created%20%28see%20%5BBranching%5D%28https%3A%2F%2Fgithub.com%2Fmicrosoft%2Freverse-proxy%2Fblob%2Fmain%2Fdocs%2Foperations%2FBranching.md%29%29%0A-%20%5B%20%5D%20Ensure%20the%20%60Version.props%60%20has%20the%20%60PreReleaseVersionLabel%60%20updated%20to%20the%20next%20preview%0A-%20%5B%20%5D%20Identify%20and%20validate%20the%20build%20on%20the%20%60microsoft-reverse-proxy-official%60%20pipeline%0A-%20%5B%20%5D%20Release%20the%20build%0A-%20%5B%20%5D%20Tag%20the%20commit%0A-%20%5B%20%5D%20Draft%20release%20notes%0A-%20%5B%20%5D%20Update%20samples%20to%20use%20updated%20package%20versions%0A-%20%5B%20%5D%20Publish%20the%20docs%0A-%20%5B%20%5D%20Publish%20release%20notes%0A-%20%5B%20%5D%20Close%20the%20%5Bold%20milestone%5D%28https%3A%2F%2Fgithub.com%2Fmicrosoft%2Freverse-proxy%2Fmilestones%29%0A-%20%5B%20%5D%20Announce%20on%20social%20media%0A-%20%5B%20%5D%20Set%20the%20preview%20branch%20to%20protected%0A-%20%5B%20%5D%20Delete%20the%20%5Bprevious%20preview%20branch%5D%28https%3A%2F%2Fgithub.com%2Fmicrosoft%2Freverse-proxy%2Fbranches%29%0A-%20%5B%20%5D%20Request%20source%20code%20archival).

## Ensure there's a release branch created.

See [Branching](Branching.md):
- Make the next preview branch.
- Update the branding in main.
- Update the global.json runtime and SDK versions in main.

## Identify the Final Build

First, identify the final build of the [`microsoft-reverse-proxy-official` Azure Pipeline](https://dev.azure.com/dnceng/internal/_build?definitionId=809&_a=summary) (on dnceng/internal). The final build will be the latest successful build **in the relevant `release/x` branch**. Use the "Branches" tab on Azure DevOps to help identify it. If the branch hasn't been mirrored yet (see [code-mirror pipeline](https://dev.azure.com/dnceng/internal/_build?definitionId=16&keywordFilter=microsoft%20reverse-proxy)) and there are no outstanding changesets in the branch, the build of the corresponding commit from the main branch can be used.

Once you've identified that build, click in to the build details.

## Validate the Final Build

At this point, you can perform any validation that makes sense. At a minimum, we should validate that the sample can run with the candidate packages. You can download the final build using the "Artifacts" which can be accessed under "Related" in the header:

![image](https://user-images.githubusercontent.com/7574/81447119-e4204800-9130-11ea-8952-9a0f9831f678.png)

The packages can be accessed from the `PackageArtifacts` artifact:

![image](https://user-images.githubusercontent.com/7574/81447168-fef2bc80-9130-11ea-8aa0-5a83d90efa0d.png)

### Consume .nupkg
- Visual Studio: Place it in a local folder and add that folder as a nuget feed in Visual Studio.
- Command Line: `dotnet nuget add source <directory> -n local`

Walk through the [Getting Started](https://microsoft.github.io/reverse-proxy/articles/getting_started.html) instructions and update them in the release branch as needed.

Also validate any major new scenarios this release and their associated docs.

## Release the build

Once validation has been completed, it's time to release. Go back to the Final Build in Azure DevOps. It's probably good to triple-check the version numbers of the packages in the artifacts against whatever validation was done at this point.

Select "Release" from the triple-dot menu in the top-right of the build details page:

![image](https://user-images.githubusercontent.com/7574/81447354-55f89180-9131-11ea-84bc-0138d7b211e4.png)

Verify the Release Pipeline selected is `microsoft-reverse-proxy-release`, that the `NuGet.org` stage has a blue border (meaning it will automatically deploy) and that the build number under Artifacts matches the build number of the final build (it will not match the package version). The defaults selected by Azure Pipelines should configure everything correctly but it's a good idea to double check.

![image](https://user-images.githubusercontent.com/7574/81447433-76c0e700-9131-11ea-9e8b-e4984ab7c31a.png)
