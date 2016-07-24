
if($env:APPVEYOR_REPO_BRANCH -ne 'stable')
{
    Write-Host "Skipping deployment for current branch '$($env:APPVEYOR_REPO_BRANCH)'"
    exit(0)
}

Write-Host "Installing nuget package provider"
Install-PackageProvider NuGet -MinimumVersion '2.8.5.201' -Force

Import-Module ".\Elastico\Elastico.psd1"
$cmdlets = (Get-Command -Module Elastico).Name

Write-Host "Updating Manifest"
Update-ModuleManifest -Path .\Elastico\Elastico.psd1 -ModuleVersion "$($env:APPVEYOR_BUILD_VERSION)"
Update-ModuleManifest -Path .\Elastico\Elastico.psd1 -CmdletsToExport $cmdlets

Write-Host "Deploying module Elastico v$($env:APPVEYOR_BUILD_VERSION) to powershellgallery"
Publish-Module -Path .\Elastico -NuGetApiKey "$($env:powershellgallery_apikey)"
