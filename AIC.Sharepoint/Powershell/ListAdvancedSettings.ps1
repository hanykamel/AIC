cls
if ((Get-PSSnapin | Where { $_.Name -eq "Microsoft.SharePoint.PowerShell" }) -eq $null) {
    Add-PSSnapin Microsoft.SharePoint.PowerShell;
}

$Urls = @("http://10.0.11.23:5555/en/", "http://10.0.11.23:5555/ar/");
$Site="http://10.0.11.23:5555"
#$Docs = @("Photo Gallery", "Resources", "Video Gallery");
#$ArDocs = @("مكتبة الصور", "المصادر", "مكتبة الفيديوهات");

#$Lists = @("Calendar", "CertificationTypes", "CompititionStatues", "Competitions", "CouncilMembers", "AIInNumbers",
#    "AIPillars", "EventTypes", "Events", "EventParticipations", "FAQCategories",
 #   "FAQS", "AIStartups", "Ecosystem", "Gender", "GenericContent",
  #  "Governorates", "HackathonActivities", "Interests", "InvestmentOpportunities", "MainBanner",
   # "MainSectors", "MainTechnologies", "MemberCategories", "Nationalities",
   # "News", "Occupations", "PartnerTypes", "Partners", "ProgramTypes",
   # "Programs", "ProjectTypes", "Projects", "RelatedLinks", "StartUpResources",
   # "TargetedAudiences", "Types", "Vacancies", "Winners")


function UpdateList([Microsoft.SharePoint.SPWeb]$Web, [string]$ListName) {
    Try {
        #Get the List
        $list = $Web.GetList($Site + $ListName) #Get-SPList $URL $ListName
        Write-Host $list.Title -ForegroundColor Cyan
        #Set Draft item security - Who should see draft items in this list? 
        $list.DraftVersionVisibility = 2 
        #Any user who can read items = 0. Only users who can edit items = 1. Only users who can approve items (and the author of the item) = 2 
        
        #Enable Attachments: Applicable Only for Lists!
        $List.EnableModeration=$true
        $List.EnableAttachments = $False
        $List.EnableVersioning = $true
        $List.MajorVersionLimit = 10
        #Update list settings 
        $list.Update()
        Write-host -f Green "Updated Successfully"
    }
    catch {
        Write-Host $_.Exception.Message -ForegroundColor Red
    } 
}

function UpdateDocuments ([Microsoft.SharePoint.SPWeb]$Web, [string]$ListName) {
    Try {
        #Get the Doc
        $Doc = $Web.Lists[$ListName]
        Write-Host $Doc.Title -ForegroundColor Cyan
        #Set Draft item security - Who should see draft items in this Doc? 
        $Doc.DraftVersionVisibility = 2 
        #Any user who can read items = 0. Only users who can edit items = 1. Only users who can approve items (and the author of the item) = 2 
        
        #Enable Attachments: Applicable Only for Lists!
        $Doc.EnableAttachments = $False
        $Doc.EnableVersioning = $true
        $Doc.MajorVersionLimit = 10
        #Update Doc settings 
        $Doc.Update()
        Write-host -f Green "Updated Successfully"
    }
    catch {
        Write-Host $_.Exception.Message -ForegroundColor Red
    }
}


for ($i = 0; $i -lt $Urls.Count; $i++) {
    $Web = Get-SPWeb $Urls[$i]
    $ListCollection = $Web.lists | Where-Object  { ($_.hidden -eq $false) -and ($_.IsSiteAssetsLibrary -eq $false) -and ($_.Title -ne 'Pages') -and ($_.Title -ne 'Images') -and ($_.Title -ne 'Documents') -and ($_.BaseTemplate -ne 'Tasks') -and ($_.BaseTemplate -ne 850) }


    Write-Host 'web'+ $Web.Title -ForegroundColor green

    #Iterate through All lists and Libraries
ForEach ($List in $ListCollection)
{
    Write-Host 'list ' $List.DefaultViewUrl -ForegroundColor green

        UpdateList $Web $List.DefaultViewUrl
    
}
  
    $Web.Dispose()
}

#$EnWeb = Get-SPWeb "http://10.0.11.23:3333/en/"
#for ($i = 0; $i -lt $Docs.Count; $i++) {
#    UpdateDocuments $EnWeb $Docs[$i]
#}
#$EnWeb.Dispose()

#$ArWeb = Get-SPWeb "http://10.0.11.23:3333/ar/"
#for ($i = 0; $i -lt $ArDocs.Count; $i++) {
 #   UpdateDocuments $ArWeb $ArDocs[$i]
#}
#$ArWeb.Dispose()



#Read more: https://www.sharepointdiary.com/2013/01/change-list-settings-programmatically-powershell.html#ixzz6p1zkSqfE