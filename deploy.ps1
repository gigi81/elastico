
if($env:APPVEYOR_REPO_BRANCH -ne 'stable')
{
    Write-Host "Skipping deployment for current branch '$($env:APPVEYOR_REPO_BRANCH)'"
    exit(0)
}

Write-Host "Installing nuget package provider"
Install-PackageProvider NuGet -MinimumVersion '2.8.5.201' -Force

Write-Host "Deploying module Elastico v$($env:APPVEYOR_BUILD_VERSION) to powershellgallery"
Publish-Module -Path .\Elastico -NuGetApiKey "$($env:powershellgallery_apikey)" -Tags @("Elasticsearch")
