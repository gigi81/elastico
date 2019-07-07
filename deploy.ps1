Write-Host "Installing nuget package provider"
Install-PackageProvider NuGet -MinimumVersion '2.8.5.201' -Force

Write-Host "Importing module"
Import-Module ".\Elastico\Elastico.psd1"
$cmdlets = (Get-Command -Module Elastico).Name

if(-not [string]::IsNullOrEmpty($env:BUILD_BUILDNUMBER)){ $version = $env:BUILD_BUILDNUMBER }
elseif(-not [string]::IsNullOrEmpty($env:APPVEYOR_BUILD_VERSION)) { $version = $env:APPVEYOR_BUILD_VERSION }
else { $version = '0.0.1' }

Write-Host "Updating Manifest $version"
$path = '.\Elastico\Elastico.psd1'
(Get-Content -Path $path).Replace("ModuleVersion = '1.0.0'", "ModuleVersion = '$version'") | Set-Content -Path $path
#Update-ModuleManifest -Path $path -ModuleVersion $version -CmdletsToExport $cmdlets

if($env:APPVEYOR_REPO_BRANCH -eq 'stable' -or $env:BUILD_SOURCEBRANCHNAME -eq 'stable')
{
    Write-Host "Deploying module Elastico $version to powershellgallery"
    Publish-Module -Path .\Elastico -NuGetApiKey "$($env:powershellgallery_apikey)"
}
