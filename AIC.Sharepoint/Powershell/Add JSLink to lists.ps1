cls
if ((Get-PSSnapin | Where { $_.Name -eq "Microsoft.SharePoint.PowerShell" }) -eq $null) {
    Add-PSSnapin Microsoft.SharePoint.PowerShell;
}

# web, listName, JSLikField, jsLinkFile



Function Add-JsLikToList($WebLang, $ListName, $JSLinkField, $JsLinkFile) {   
    Try {

        $Web = Get-SPWeb -Identity "http://10.0.11.23:5555/$WebLang"

        Write-Host 'web'+ $Web.Title -ForegroundColor green

        $List = $Web.GetList($Web.Url + "/Lists/" + $ListName);
        Write-Host $List.Title -ForegroundColor Cyan  

        $siteColumn = $List.Fields.GetFieldByInternalName($JSLinkField)

        Write-Host $siteColumn.SchemaXml
        Write-Host $siteColumn.JSLink
        
        $siteColumn.JSLink = "~siteCollection/Style Library/JSlink/" + $JsLinkFile
        $siteColumn.Update();
        
        Write-Host $siteColumn.SchemaXml -ForegroundColor Yellow
        Write-Host $siteColumn.JSLink -ForegroundColor Yellow
        Write-host -f Green "Added Successfully"
    }
    catch {
        Write-Host $_.Exception.Message -ForegroundColor Red
    }    
}


#Add-JsLikToList 'en' "FAQs" "Category" "FAQJSLink.js"
#Add-JsLikToList 'ar' "FAQs" "Category" "FAQJSLink.js"
#Add-JsLikToList 'en' "APADirectory" "District" "APADirectoryJSLink.js"
#Add-JsLikToList 'ar' "APADirectory" "District" "APADirectoryJSLink.js"
#Add-JsLikToList 'en' "Lectures" "Hall" "LecturesJSLink.js"
#Add-JsLikToList 'ar' "Lectures" "Hall" "LecturesJSLink.js"
#Add-JsLikToList 'en' "GenericContent" "Long" "GoogleJSLink.js"
#Add-JsLikToList 'ar' "GenericContent" "Long" "GoogleJSLink.js"