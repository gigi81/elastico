@{

ModuleVersion = '1.0.0'
GUID = 'bd4390dc-a8ad-4bce-8d69-f53ccf8e4163'
Author = 'Luigi Grilli'
CompanyName = 'Luigi Grilli'
Copyright = 'Copyright (c) 2017 - 2019 Luigi Grilli. All rights reserved.'
Description = 'Powershell module for working with elasticsearch clusters'
PowerShellVersion = '5.1'
DotNetFrameworkVersion = '4.6.1'
CLRVersion = '4.0'

CompatiblePSEditions = @('Desktop', 'Core')

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = if($PSEdition -eq 'Core')
{
    'coreclr\V2\Elasticsearch.Powershell.V2.dll',
    'coreclr\V5\Elasticsearch.Powershell.V5.dll',
    'coreclr\V6\Elasticsearch.Powershell.V6.dll',
    'coreclr\V7\Elasticsearch.Powershell.V7.dll'
}
else # Desktop
{
    'clr\V2\Elasticsearch.Powershell.V2.dll',
    'clr\V5\Elasticsearch.Powershell.V5.dll',
    'clr\V6\Elasticsearch.Powershell.V6.dll',
    'clr\V7\Elasticsearch.Powershell.V7.dll'
}

# HelpInfo URI of this module
HelpInfoURI = 'http://www.github.com/gigi81/elastico'

# Name of the Windows PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the Windows PowerShell host required by this module
# PowerShellHostVersion = ''

# Processor architecture (None, X86, Amd64) required by this module
# ProcessorArchitecture = ''

# Modules that must be imported into the global environment prior to importing this module
# RequiredModules = @()

# Assemblies that must be loaded prior to importing this module
# RequiredAssemblies = @()

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
# FormatsToProcess = @()

# Functions to export from this module
FunctionsToExport = ''

# Cmdlets to export from this module
CmdletsToExport = 'Get-ElasticV2ClusterHealth', 'Get-ElasticV2Index', 
               'Get-ElasticV2IndexSettings', 'Get-ElasticV2NodeInfo', 
               'Get-ElasticV2Repository', 'Get-ElasticV2RepositorySettings', 
               'Get-ElasticV2Snapshot', 'Get-ElasticV5ClusterHealth', 
               'Get-ElasticV5Index', 'Get-ElasticV5IndexSettings', 
               'Get-ElasticV5NodeInfo', 'Get-ElasticV5Repository', 
               'Get-ElasticV5RepositorySettings', 'Get-ElasticV5Snapshot', 
               'Get-ElasticV6ClusterHealth', 'Get-ElasticV6Index', 
               'Get-ElasticV6IndexSettings', 'Get-ElasticV6NodeInfo', 
               'Get-ElasticV6Repository', 'Get-ElasticV6RepositorySettings', 
               'Get-ElasticV6Snapshot', 'Get-ElasticV7ClusterHealth', 
               'Get-ElasticV7Index', 'Get-ElasticV7IndexSettings', 
               'Get-ElasticV7NodeInfo', 'Get-ElasticV7Repository', 
               'Get-ElasticV7RepositorySettings', 'Get-ElasticV7Snapshot', 
               'New-ElasticV2Index', 'New-ElasticV2RepositoryFileSystem', 
               'New-ElasticV2Snapshot', 'New-ElasticV5Index', 
               'New-ElasticV5RepositoryFileSystem', 'New-ElasticV5Snapshot', 
               'New-ElasticV6Index', 'New-ElasticV6RepositoryFileSystem', 
               'New-ElasticV6Snapshot', 'New-ElasticV7Index', 
               'New-ElasticV7RepositoryFileSystem', 'New-ElasticV7Snapshot', 
               'Remove-ElasticV2Index', 'Remove-ElasticV2Repository', 
               'Remove-ElasticV2Snapshot', 'Remove-ElasticV5Index', 
               'Remove-ElasticV5Repository', 'Remove-ElasticV5Snapshot', 
               'Remove-ElasticV6Index', 'Remove-ElasticV6Repository', 
               'Remove-ElasticV6Snapshot', 'Remove-ElasticV7Index', 
               'Remove-ElasticV7Repository', 'Remove-ElasticV7Snapshot', 
               'Search-ElasticV2', 'Search-ElasticV5', 'Search-ElasticV6', 
               'Search-ElasticV7', 'Set-ElasticV2IndexSettings', 
               'Set-ElasticV5IndexSettings', 'Set-ElasticV6IndexSettings', 
               'Set-ElasticV7IndexSettings'

# Variables to export from this module
VariablesToExport = '*'

# Aliases to export from this module
AliasesToExport = '*'

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
# FileList = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess
# PrivateData = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

PrivateData = @{
    PSData = @{
        # The primary categorization of this module (from the TechNet Gallery tech tree).
        Category = "Databases"

        # Keyword tags to help users find this module via navigations and search.
        Tags = 'elasticsearch','database'

        # The web address of an icon which can be used in galleries to represent this module
        IconUri = "https://raw.githubusercontent.com/elastic/elasticsearch-net/master/build/nuget-icon.png"

        # The web address of this module's project or support homepage.
        ProjectUri = "http://www.github.com/gigi81/elastico"

        # The web address of this module's license. Points to a page that's embeddable and linkable.
        LicenseUri = "https://raw.githubusercontent.com/gigi81/elastico/master/license.md"

        # Release notes for this particular version of the module
        # ReleaseNotes = False

        # If true, the LicenseUrl points to an end-user license (not just a source license) which requires the user agreement before use.
        # RequireLicenseAcceptance = ""

        # Indicates this is a pre-release/testing version of the module.
        IsPrerelease = 'False'
    }
}

}

