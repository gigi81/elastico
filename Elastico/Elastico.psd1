@{

RootModule = 'Elastico.psm1'
ModuleVersion = '0.1.0'
GUID = 'bd4390dc-a8ad-4bce-8d69-f53ccf8e4163'
Author = 'Luigi Grilli'
CompanyName = 'Luigi Grilli'
Copyright = 'Copyright (c) 2016 Luigi Grilli. All rights reserved.'
Description = 'Powershell module for working with elasticsearch clusters'
PowerShellVersion = '4.0'
DotNetFrameworkVersion = '4.5'
CLRVersion = '4.0'
# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @('bin\Elasticsearch.Powershell.dll')
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
FunctionsToExport = 'Get-Function'

# Cmdlets to export from this module
CmdletsToExport = '*'

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
}

