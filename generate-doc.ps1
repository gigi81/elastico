git clone https://github.com/ChaseFlorell/psDoc.git

Import-Module ".\Elastico\Elastico.psd1"

Get-Command -Module Elastico

cd .\psDoc\src
.\psdoc.ps1 -ModuleName Elastico
