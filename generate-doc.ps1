Import-Module ".\Elastico\Elastico.psd1"

Get-Command -Module Elastico | Format-Table -Property Name,Version

cd .\psDoc\src
.\psdoc.ps1 -ModuleName Elastico
cd ..
cd ..
