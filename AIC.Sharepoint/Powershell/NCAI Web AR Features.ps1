 cls
if((Get-PSSnapin | Where {$_.Name -eq "Microsoft.SharePoint.PowerShell"}) -eq $null) 
{
    Add-PSSnapin Microsoft.SharePoint.PowerShell;
}

$arWeb=Get-SPWeb -Identity "http://10.0.11.23:3333/ar/"

Write-Host 'web' $arWeb.Title -ForegroundColor green

Write-Host $arWeb.url -ForegroundColor Cyan
Write-Host '---------------------Deactivating Features----------------------' -ForegroundColor white    

Disable-SPFeature -Identity "34da765d-42f7-4b90-87b0-f2aa3a45dea8" -Url $arWeb.Url –Confirm:$false
Write-Host 'web '+ $arWeb.Title + ' EventReceivers.APA Feature Deacivated' -ForegroundColor green

Disable-SPFeature -Identity "19eceab2-6233-41cd-9a2d-37b7ec2ff2c6" -Url $arWeb.Url –Confirm:$false
Write-Host 'web '+ $arWeb.Title + 'StartUpResources.APA Feature Deacivated' -ForegroundColor green
  
Disable-SPFeature -Identity "37e03f10-81f7-4afa-919b-2c1a7a5287e9" -Url $arWeb.Url –Confirm:$false
Write-Host 'web '+ $arWeb.Title + 'Synchronization.APA Feature Deacivated' -ForegroundColor green

  
########################################
Write-Host '-------------------Activating Features------------' -ForegroundColor Magenta

Enable-SPFeature -Identity "34da765d-42f7-4b90-87b0-f2aa3a45dea8" -Url $arWeb.Url     –Confirm:$false
Write-Host 'web '+ $arWeb.Title + ' EventReceivers.APA Feature acivated' -ForegroundColor green

Enable-SPFeature -Identity "19eceab2-6233-41cd-9a2d-37b7ec2ff2c6" -Url $arWeb.Url     –Confirm:$false
Write-Host 'web '+ $arWeb.Title + 'StartUpResources.APA Feature acivated' -ForegroundColor green

Enable-SPFeature -Identity "37e03f10-81f7-4afa-919b-2c1a7a5287e9" -Url $arWeb.Url     –Confirm:$false
Write-Host 'web '+ $arWeb.Title + 'Synchronization.APA Feature acivated' -ForegroundColor green

Remove-PSSnapin Microsoft.SharePoint.PowerShell;