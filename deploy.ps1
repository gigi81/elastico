
if($env:APPVEYOR_REPO_BRANCH -ne 'stable')
{
    Write-Host "Skipping deployment for current branch '$($env:APPVEYOR_REPO_BRANCH)'"
    exit(0)
}

Write-Host "Installing nuget package provider"
Install-PackageProvider NuGet -MinimumVersion '2.8.5.201' -Force

Write-Host "Deploying module to powershellgallery"
Publish-Module -Path .\dist -NuGetApiKey "$($env:powershellgallery_apikey)"
