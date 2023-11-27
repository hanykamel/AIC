<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AICCtrlAdditionalPageHead.ascx.cs" Inherits="APA.Sharepoint.CONTROLTEMPLATES.AIC.AICCtrlAdditionalPageHead" %>

<script type="text/javascript" src="/_catalogs/masterpage/AIC/JS/jquery-3.5.1.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/AIC/JS/jquery-ui.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/AIC/JS/jquery.jtable.min.js"></script>
<script> 
    $("#ctl00_PlaceHolderMain_ctl04_RptControls_btnOK").click(function () {
        var newFormUrl = _spPageContextInfo.listUrl.toLocaleLowerCase();
        var okBtn = document.getElementById("ctl00_PlaceHolderMain_ctl04_RptControls_btnOK");
        var uploadedFile = document.getElementById("ctl00_PlaceHolderMain_UploadDocumentSection_ctl05_InputFile").files[0];

        // check for Mailing List: pdf only
        if (newFormUrl.indexOf('/MailingList') > -1) {
            var errMsg;
            errMsg = "Allowed files are PDF and one file only";

            if (uploadedFile) {
                if (uploadedFile.type.toLocaleLowerCase().indexOf('pdf') > -1) {
                    okBtn.removeAttribute("disabled");
                    showErr("", false);
                }
                else {
                    okBtn.setAttribute("disabled", "disabled")
                    showErr(errMsg, true);
                }
            }
            else {
                okBtn.setAttribute("disabled", "disabled");
                showErr("", false);
            }

        }
    });

    function showErr(errMsg, show) {
        var errNodeId = "fileErr";
        var largeDivTr = document.getElementById("ctl00_PlaceHolderMain_UploadDocumentSection_ctl05_ctl00").parentNode.parentNode;
        var errNode = document.getElementById(errNodeId);
        if (show) {
            if (!errNode) {
                errNode = document.createElement('tr');
                errNode.setAttribute("id", errNodeId);
                errNode.setAttribute("style", "display:none");
                largeDivTr.parentNode.insertBefore(errNode, largeDivTr);
            }
            errNode.innerHTML = "<td><div><label class='ms-error'> " + errMsg + " </label></div> </td>";
            errNode.style.display = "inline";
        }
        else {
            if (errNode)
                errNode.style.display = "none"
        }
    }

</script>