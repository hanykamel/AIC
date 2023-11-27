cls
if ((Get-PSSnapin | Where { $_.Name -eq "Microsoft.SharePoint.PowerShell" }) -eq $null) {
    Add-PSSnapin Microsoft.SharePoint.PowerShell;
}

$Urls = @("http://10.0.11.23:5555/en/", "http://10.0.11.23:5555/ar/");
$Site="http://10.0.11.23:5555"

#$Lists = @("Calendar", "CertificationTypes", "CompititionStatues", "Competitions", "CouncilMembers", "AIInNumbers",
#    "AIPillars", "EventTypes", "Events", "EventParticipations", "FAQCategories",
 #   "FAQS", "AIStartups", "Ecosystem", "Gender", "GenericContent",
  #  "Governorates", "HackathonActivities", "Interests", "InvestmentOpportunities", "MainBanner",
  #  "MainSectors", "MainTechnologies", "MemberCategories", "Nationalities",
  #  "News", "Occupations", "PartnerTypes", "Partners", "ProgramTypes",
  #  "Programs", "ProjectTypes", "Projects", "RelatedLinks", "StartUpResources",
  #  "TargetedAudiences", "Types", "Vacancies", "Winners")

$Desc = @("The maximum length is 100 characters.", "الحد الأقصى 100 حرف.")


function UpdateALLListItems ([Microsoft.SharePoint.SPList]$List) {
    foreach ($item in $List.Items) {
        $string = $item["Title"];
        Write-Host $string -ForegroundColor Cyan
        if($string.Length -gt 80){
            Write-Host 'Title length is more than 80 chars' -ForegroundColor Red
            $item["Title"] = $string.Substring(0,80)
            Write-Host $item["Title"] -ForegroundColor Green
            $item.Update();
        }
    }
}
function UpdateALLListItemsBrief ([Microsoft.SharePoint.SPList]$List) {
    foreach ($item in $List.Items) {
        $string = $item["APABriefText"];
        Write-Host $string -ForegroundColor Cyan
        if($string.Length -gt 100){
            Write-Host 'Title length is more than 100 chars' -ForegroundColor Red
            $item["Title"] = $string.Substring(0,99)
            Write-Host $item["Title"] -ForegroundColor Green
            $item.Update();
        }
    }
}

function UpdateList([Microsoft.SharePoint.SPWeb]$Web, [string]$ListName, [string]$Desc) {
    Try {
        #Get the List
        $list = $Web.GetList($Site + $ListName)
        Write-Host $list.Title -ForegroundColor Cyan
       
      # $siteColumn = $list.Fields.GetFieldByInternalName("Title")
      # Write-Host $siteColumn.SchemaXml
       #Write-Host $siteColumn.MaxLength
       #Write-Host $siteColumn.Description
       
       #$siteColumn.MaxLength = 80
       #$siteColumn.Description = $Desc
       
       #$schema = [xml] $siteColumn.SchemaXml
       #$schema.Field.Attributes["Description"].Value = $Desc
       #$siteColumn.SchemaXml = $schema.OuterXml
       
       #$siteColumn.Update($true);
       
       #Write-Host $siteColumn.SchemaXml
       #Write-Host $siteColumn.MaxLength
       #Write-Host $siteColumn.Description
       $siteColumn = $list.Fields.GetFieldByInternalName("APABriefText")
       if($siteColumn -ne $null){
        $siteColumn.MaxLength = 100
       $siteColumn.Description = $Desc
       
       $schema = [xml] $siteColumn.SchemaXml
       $schema.Field.Attributes["Description"].Value = $Desc
       $siteColumn.SchemaXml = $schema.OuterXml
       
       $siteColumn.Update($true);
       
       Write-Host $siteColumn.SchemaXml
       Write-Host $siteColumn.MaxLength
       Write-Host $siteColumn.Description
       }

       Write-host -f Green "Updated Successfully"

        #UpdateALLListItems $list;
        UpdateALLListItemsBrief $list
    }
    catch {
        Write-Host $_.Exception.Message -ForegroundColor Red
    } 
}



for ($i = 0; $i -lt $Urls.Count; $i++) {
    $Web = Get-SPWeb $Urls[$i]
    $ListCollection = $Web.lists | Where-Object  { ($_.hidden -eq $false) -and ($_.IsSiteAssetsLibrary -eq $false) -and ($_.Title -ne 'Pages') -and ($_.Title -ne 'Images') -and ($_.Title -ne 'Documents') -and ($_.BaseTemplate -ne 'Tasks') -and ($_.BaseTemplate -ne 850) }

    Write-Host 'web'+ $Web.Title -ForegroundColor green
    $TitleDesc = $Desc[$i]

    ForEach ($List in $ListCollection)
{
    Write-Host 'list ' $List.DefaultViewUrl -ForegroundColor green

        UpdateList $Web $List.DefaultViewUrl $TitleDesc
    
}

    
    $Web.Dispose()
}

