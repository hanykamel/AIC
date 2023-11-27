
 cls
if((Get-PSSnapin | Where {$_.Name -eq "Microsoft.SharePoint.PowerShell"}) -eq $null) 
{
    Add-PSSnapin Microsoft.SharePoint.PowerShell;
}



 $spSiteCollection = Get-SPSite "http://10.0.11.23:3333"


     Write-Host $spSiteCollection.url -ForegroundColor Cyan
     Write-Host '---------------------Deactivating Features----------------------' -ForegroundColor white
     Write-Host '---------------------Deactivating DocumentsTypes Features----------------------' -ForegroundColor Cyan

     Disable-SPFeature -Identity "8e5ef8be-f0a9-4d0d-8907-99a71a1748b9" -Url $spSiteCollection.Url –Confirm:$false
     Write-Host 'site '+ $spSiteCollection.url + ' DocumentsTypes.APA Feature Deacivated' -ForegroundColor green

 ########################################

 Write-Host '-------------------Activating Features------------' -ForegroundColor Magenta
     Write-Host '---------------------Activating DocumentsTypes Features----------------------' -ForegroundColor Cyan


    Enable-SPFeature -Identity "8e5ef8be-f0a9-4d0d-8907-99a71a1748b9" -Url $spSiteCollection.Url   –Confirm:$false
    Write-Host 'site '+ $spSiteCollection.url + ' DocumentsTypes.APA Feature acivated' -ForegroundColor green

    

Remove-PSSnapin Microsoft.SharePoint.PowerShell;