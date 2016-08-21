[![Build status master](https://img.shields.io/appveyor/ci/gigi81/elastico/master.svg?label=master)](https://ci.appveyor.com/project/gigi81/elastico/branch/master)
[![Build status stable](https://img.shields.io/appveyor/ci/gigi81/elastico/stable.svg?label=stable)](https://ci.appveyor.com/project/gigi81/elastico/branch/stable)

# Elastico

Powershell module for Elasticsearch.

Install
============

You can install the module from powershell gallery with the following command:
```powershell
Install-Module Elastico
```

Features
============

The current version of the module supports the following functionalities:
- list/add/remove indices
- list/add/remove repositories (for backup)
- list/add/remove snapshots (for backup)
- get cluster health
- get cluster info
- search
- get/set index settings

Documentation
============

Documentation is available on [github pages](https://gigi81.github.io/elastico/#Get-ElasticClusterHealth)

Elasticsearch Versions(s) Compatibility
============

The module supports versions 1, 2 and 5 (currently in alpha stage) of elasticsearch. As long as the server API can change between versions, for compatibility reasons, the cmdlets are specific to the single versions of elasticsearch.

So for example, instead of having a single Get-ElasticClusterHealt cmdlet, there are 3 cmdlets, more specifically:
- Get-ElasticV1ClusterHealth
- Get-ElasticV2ClusterHealth
- Get-ElasticV5ClusterHealth.

In order to avoid compatibility issues, you will need to know in advance the version of elasticsearch that you are dealing with, and use the relevant cmdlet.


Usage Examples
============

```powershell
Get-ElasticV2ClusterHealth -Node "http://localhost:9200"

ActivePrimaryShards  : 0
ActiveShards         : 0
Name                 : elasticsearch
InitializingShards   : 0
NumberOfDataNodes    : 1
NumberOfNodes        : 1
NumberOfPendingTasks : 0
RelocatingShards     : 0
UnassignedShards     : 0
Status               : green
```

```powershell
Get-ElasticV2Index -Node "http://localhost:9200" | Format-Table -Property Name,Status

Name      Status
----      ------
testindex open
```

```powershell
New-ElasticV2Index -Node "http://localhost:9200" -Index "testindex"

DocsCount        : 0
DocsDeleted      : 0
Health           : red
Name             : testindex
Primary          : 5
PrimaryStoreSize :
Replica          : 1
Status           : open
StoreSize        :
TotalMemory      :
```
