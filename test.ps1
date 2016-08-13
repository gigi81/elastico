Import-Module ".\Elastico\Elastico.psd1"
$nodes = @(
    "http://localhost:9200"
)

# Get-Command -Module Elastico | Format-Table -Property Name,Version

# Get-Help Get-ElasticV2ClusterHealth

Get-ElasticV2ClusterHealth -Verbose

# Get-ElasticV2ClusterHealt -Index ".kibana" -Verbose

# \\\\WIN2012\\Backup\\
# New-ElasticV2RepositoryFileSystem -Name "test1" -Location "test1" -Compress $true -Verbose

# Get-ElasticV2RepositorySettings

# New-ElasticV2Snapshot -Repository "test1" -Name "snap1"

# Get-ElasticV2Snapshot -Repository "test1"

# Get-ElasticV2NodeInfo -Verbose

# Get-ElasticV2ClusterHealt | Get-ElasticV2Node

#Get-ElasticV2ClusterHealtHealth "localhost:9200" -Verbose

#Get-ElasticV2IndicesHealth

# Get-ElasticV2Index

#Get-ElasticV2Index -Index ".kibana,logstash-2016.06.18"

#Get-ElasticV2Index -Index @(".kibana", "logstash-2016.06.18")

# Get-ElasticV2Index -Index "logstash-*" | Remove-ElasticV2Index

# Remove-ElasticV2Index -Index "logstash-*"

# New-ElasticV2Index -Index "test1234"

#Search-ElasticV2 -Index "logstash-*" | Where-Object { $_.EventID -eq 4002 }

# Search-ElasticV2 -Index "logstash-*" -Query "EventID: 7036" -Size 2 -Verbose

# Search-ElasticV2 -Node $nodes -Query "EventID: 7036" -Fields @("Hostname", "EventID", "Version") -Verbose

#Search-ElasticV2 -Node $nodes -Query "EventID: 7036" -Fields "Hostname,EventID" -Verbose

# Get-ElasticV2IndexSettings -Index ".kibana"

# Set-ElasticV2IndexSettings -Index ".kibana" -Settings @{ "number_of_shards" = 2 } -Verbose

Get-ElasticV2IndexSettings -Index ".kibana"