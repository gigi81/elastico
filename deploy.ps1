Write-Host "Installing nuget package provider"
Install-PackageProvider NuGet -MinimumVersion '2.8.5.201' -Force

Write-Host "Importing module"
Import-Module ".\Elastico\Elastico.psd1"
$cmdlets = (Get-Command -Module Elastico).Name

if(-not [string]::IsNullOrEmpty($env:BUILD_BUILDNUMBER)){ $version = $env:BUILD_BUILDNUMBER }
elseif(-not [string]::IsNullOrEmpty($env:APPVEYOR_BUILD_VERSION)) { $version = $env:APPVEYOR_BUILD_VERSION }
else { $version = '1.0.0' }

Write-Host "Updating Manifest $version"
Update-ModuleManifest -Path .\Elastico\Elastico.psd1 -ModuleVersion $version
Update-ModuleManifest -Path .\Elastico\Elastico.psd1 -CmdletsToExport $cmdlets

if($env:APPVEYOR_REPO_BRANCH -eq 'stable' -or $env:BUILD_SOURCEBRANCHNAME -eq 'stable')
{
    Write-Host "Deploying module Elastico v$($env:APPVEYOR_BUILD_VERSION) to powershellgallery"
    Publish-Module -Path .\Elastico -NuGetApiKey "$($env:powershellgallery_apikey)"
}
