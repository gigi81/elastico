git clone https://github.com/ChaseFlorell/psDoc.git 2> $null

Import-Module ".\Elastico\Elastico.psd1"

Get-Command -Module Elastico

cd .\psDoc\src
.\psdoc.ps1 -ModuleName Elastico
