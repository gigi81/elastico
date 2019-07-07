IF EXIST Elastico\coreclr  rmdir /q /s Elastico\coreclr
IF EXIST Elastico\clr rmdir /q /s Elastico\clr

dotnet restore
dotnet build -c Release

dotnet publish src\Elasticsearch.Powershell.V2\Elasticsearch.Powershell.V2.csproj -c Release -o ..\..\Elastico\coreclr\V2 -f netstandard2.0
dotnet publish src\Elasticsearch.Powershell.V5\Elasticsearch.Powershell.V5.csproj -c Release -o ..\..\Elastico\coreclr\V5 -f netstandard2.0
dotnet publish src\Elasticsearch.Powershell.V6\Elasticsearch.Powershell.V6.csproj -c Release -o ..\..\Elastico\coreclr\V6 -f netstandard2.0
dotnet publish src\Elasticsearch.Powershell.V7\Elasticsearch.Powershell.V7.csproj -c Release -o ..\..\Elastico\coreclr\V7 -f netstandard2.0

dotnet publish src\Elasticsearch.Powershell.V2\Elasticsearch.Powershell.V2.csproj -c Release -o ..\..\Elastico\clr\V2 -f net461
dotnet publish src\Elasticsearch.Powershell.V5\Elasticsearch.Powershell.V5.csproj -c Release -o ..\..\Elastico\clr\V5 -f net461
dotnet publish src\Elasticsearch.Powershell.V6\Elasticsearch.Powershell.V6.csproj -c Release -o ..\..\Elastico\clr\V6 -f net461
dotnet publish src\Elasticsearch.Powershell.V7\Elasticsearch.Powershell.V7.csproj -c Release -o ..\..\Elastico\clr\V7 -f net461
