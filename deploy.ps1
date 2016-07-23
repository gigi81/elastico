
if($env:APPVEYOR_REPO_BRANCH -ne 'stable')
{
    Write-Host "Skipping deployment for current branch '$($env:APPVEYOR_REPO_BRANCH)'"
    exit(0)
}

Write-Host "Deploying to powershellgallery"
cd dist
Publish-Module -Name Elastico -NuGetApiKey "$($env:powershellgallery_apikey)"
