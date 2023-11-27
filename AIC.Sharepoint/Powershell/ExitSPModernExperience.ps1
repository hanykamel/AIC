cls
if((Get-PSSnapin | Where {$_.Name -eq "Microsoft.SharePoint.PowerShell"}) -eq $null) {
    Add-PSSnapin Microsoft.SharePoint.PowerShell;
}

$site = Get-SPSite http://10.0.11.23:5555/

#Disable modern Lists and libraries at the Site Collection Level
$featureguid = new-object System.Guid "E3540C7D-6BEA-403C-A224-1A12EAFEE4C4"
$site.Features.Add($featureguid, $true)

#Re-enable the modern experience at the site collection Level.
#$featureguid = new-object System.Guid "E3540C7D-6BEA-403C-A224-1A12EAFEE4C4"
#$site.Features.Remove($featureguid, $true)


Remove-PSSnapin Microsoft.SharePoint.PowerShell;