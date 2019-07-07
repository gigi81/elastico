if(!Test-Path psDoc)
{
    git clone https://github.com/ChaseFlorell/psDoc.git 2> $null
}

Import-Module ".\Elastico\Elastico.psd1"

Get-Command -Module Elastico | Format-Table -Property Name,Version

cd .\psDoc\src
.\psdoc.ps1 -ModuleName Elastico
cd ..
cd ..
