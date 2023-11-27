#https://www.sharepointdiary.com/2012/12/copy-list-between-sites-powershell.html

cls
if((Get-PSSnapin | Where {$_.Name -eq "Microsoft.SharePoint.PowerShell"}) -eq $null) 
{
    Add-PSSnapin Microsoft.SharePoint.PowerShell;
} 
 
#Function to copy List or Library from One SharePoint site to another
Function CopyList([string]$SourceWebURL, [string]$TargetWebURL, [string]$ListName, [string]$BackupPath)
 {
    #Get the Source List
    $SourceList = (Get-SPWeb $SourceWebURL).Lists[$ListName]   
    #$SourceList = (Get-SPWeb $SourceWebURL).GetList($SourceWebURL+'\Lists\'+$ListName)
 
    #Export the List from Source web
    Export-SPweb $SourceWebURL -ItemUrl $SourceList.DefaultViewUrl -IncludeUserSecurity -IncludeVersions All -path ($BackupPath + $ListName + ".cmp") -nologfile
 
   #Import the List to Target Web
   import-spweb $TargetWebURL -IncludeUserSecurity -path ($BackupPath + $ListName + ".cmp") -nologfile -force
 }


$Web = Get-SPWeb http://10.0.11.23:5555/en/

Write-host -f Yellow "Processing Site: "$Web.URL

#Get all lists - Exclude Hidden System lists
$ListCollection = $Web.lists | Where-Object  { ($_.hidden -eq $false) -and ($_.IsSiteAssetsLibrary -eq $false) -and ($_.Title -ne 'Pages') -and ($_.Title -ne 'Images') -and ($_.Title -ne 'Documents') -and ($_.BaseTemplate -ne 'Tasks') -and ($_.BaseTemplate -ne 850) }
#@('News','Activities','Activity Types','APA Directory', 'APA Members Services')
# $Web.lists | Where-Object  { ($_.hidden -eq $false) -and ($_.IsSiteAssetsLibrary -eq $false) -and ($_.Title -ne 'Pages') -and ($_.Title -ne 'Images') -and #($_.Title -ne 'Documents') -and ($_.BaseTemplate -ne 'Tasks') -and ($_.BaseTemplate -ne 850) }

#Iterate through All lists and Libraries
ForEach ($List in $ListCollection)
{
     CopyList "http://10.0.11.23:5555/en/" "http://10.0.11.23:5555/ar/" $List "C:\Temp\"

}
 Write-Host "Coping Completed!" -f Green


