[![Build Status](https://luigigrilli.visualstudio.com/elastico/_apis/build/status/gigi81.elastico?branchName=master)](https://luigigrilli.visualstudio.com/elastico/_build/latest?definitionId=7&branchName=master)

# Elastico

Elastico is a Powershell module that allows to perform some operations with an Elasticsearch cluster. The module can run on both Windows and Linux and runs on both legacy Powershell and Powershell core.

Install
============

You can install the module from the [powershell gallery](https://www.powershellgallery.com/packages/Elastico) from the command line by running the following command:
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

The module supports versions 2, 5, 6 and 7 of elasticsearch.

Because the server API can change between versions, for compatibility reasons, the cmdlets are specific to each single version of elasticsearch supported.

So for example, instead of having a single Get-ElasticClusterHealt cmdlet, there are 3 cmdlets, more specifically:
- Get-ElasticV2ClusterHealth
- Get-ElasticV5ClusterHealth
- Get-ElasticV6ClusterHealth
- Get-ElasticV7ClusterHealth

In order to avoid compatibility issues, you will need to know in advance the version of elasticsearch that you are dealing with, and use the relevant cmdlet.

Support for version 1 of Elasticsearch has been dropped, but you can still download an older version of the module (0.6.x) if you need to connect to an elasticsearch v1.

---
**NOTE**

When using the module in powershell core, once you start using a version of the cmdlets, you cannot switch to another version as you will get an error (ex if you use V6 cmdlet and then start using a V7 cmdlets). You will need to close the current powershell session or start a new session.
This is a know issue and there is no planned fix as this is by design of powershell core.
---


Usage Examples
============

To get a cluster health status

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

To get the list of indices available in a cluster

```powershell
Get-ElasticV2Index -Node "http://localhost:9200" | Format-Table -Property Name,Status

Name      Status
----      ------
testindex open
```

To create a new index

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

To list all command supported by the module

```powershell
Get-Command -Module Elastico
```
