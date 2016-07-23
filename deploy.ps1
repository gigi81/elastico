
if($env:APPVEYOR_REPO_BRANCH -ne 'stable')
{
    Write-Host "Skipping deployment for current branch"
    exit(0)
}

cd dist
Publish-Module -Name Elastico -NuGetApiKey "$($env:powershellgallery_apikey)"
