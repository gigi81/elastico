Import-Module ".\ElasticsearchPowershell.psd1"
$nodes = @(
    "http://localhost:9200"
)

# Get-ElasticCluster -Verbose

# Get-ElasticNode

Get-ElasticCluster | Get-ElasticNode

#Get-ElasticClusterHealth "localhost:9200" -Verbose

#Get-ElasticIndicesHealth

#Get-ElasticIndex -Index ".kibana,logstash-2016.06.18"

#Get-ElasticIndex -Index @(".kibana", "logstash-2016.06.18")

# Get-ElasticIndex -Index "logstash-*" | Remove-ElasticIndex

# Remove-ElasticIndex -Index "logstash-*"

# New-ElasticIndex -Index "test1234"

#Search-Elastic -Index "logstash-*" | Where-Object { $_.EventID -eq 4002 }

#Search-Elastic -Index "logstash-*" -Query "EventID: 7036" -Size 2 -Verbose

#Search-Elastic -Nodes $nodes -Query "EventID: 7036" -Fields @("Hostname", "EventID", "Version") -Verbose

#Search-Elastic -Nodes $nodes -Query "EventID: 7036" -Fields "Hostname,EventID" -Verbose
