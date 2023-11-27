<%@ Assembly Name="AIC.Sharepoint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6b7af8fe03873811" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAppliedToInternshipDetailsUserControl.ascx.cs" Inherits="AIC.Sharepoint.Webparts.UserAppliedToInternshipDetails.UserAppliedToInternshipDetailsUserControl" %>

<script type="text/javascript" src="/_catalogs/masterpage/AIC/JS/html2canvas.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/AIC/JS/jspdf.umd.min.js"></script>

<style>
    .contain {
        background-color: #d5e5fd;
        padding: 20px;
        margin-bottom: 20px;
        border-radius: 15px;
    }

    div .info {
        color: #3C57A1;
        font-size: 20px;
        font-weight: bold;
        margin-bottom: 15px;
        font-size: large;
        font-weight: 700;
    }

    .InfoTable {
        width: 100%
    }

    .InfoTable td, .InfoTable th {
            text-align: inherit;
            padding: 0.75rem;
            vertical-align: top;
            border-top: 1px solid #F1F1F1;
            min-width: 150px;
    }    
</style>
<button class="button" type="submit" id="Export" style="font-weight: 700;padding: 15px 32px;font-size: 16px; background-color: #d5e5fd;margin-bottom: 20px;">Export</button>
<div id="contnet">
<div id="InternshipInfoContainer" class="contain" style="background-color: #e5e4e4;">
    <p class="info">Internship Info</p>
    <table id="InternshipInfo" class="InfoTable">
        <thead>
            <th>Title</th>
            <th>Internship Reference Number</th>
            <th>Location</th>
            <th>Internship Expiry Date</th>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>
<!-- End Table -->

<div class="contain">
    <p class="info">Information</p>
     <table id="GeneralInfo1" class="InfoTable">
        <thead>
            <th >Full name</th>
            <th>Email</th>
            <th>Mobile Number</th>
        </thead>
           
        <tbody>
        </tbody>
    </table>
     <table id="GeneralInfo2" class="InfoTable">
 
            <thead>
            <th>Address</th>
            <th>Birthdate</th>
            <th>Available Start Date</th>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>

<!-- End Table -->
<div id="AcademicDegreesContainer" class="contain">
    <p class="info">Academic Degrees</p>
    <table id="AcademicDegrees" class="InfoTable">
        <thead>
            <th>Degree Level</th>
            <th>Degree Date</th>
            <th>In Progress</th>
            <th>University</th>
            <th>Specialization</th>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>
<!-- End Table -->
<div id="CertificatesContainer" class="contain">
    <p class="info">Certificates</p>
    <table id="Certificates" class="InfoTable">
        <thead>
            <th>Certificate Name</th>
            <th>Certified From</th>
            <th>Certificate Date</th>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>

<!-- End Table -->
<div id="TechnicalSkillsContainer" class="contain">
    <p class="info">Technical Skills</p>
    <table id="TechnicalSkills" class="InfoTable">
        <thead>
            <th>Skill Name</th>
            <th>Years of Experience</th>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>

<!-- End Table -->
<div id="AttachmentsContainer" class="contain">
    
     <table id="OtherInfo" class="InfoTable">
        <thead>
            <th>Link to portfolio</th>
            <th>Worked with AIC As</th>
            <th>Worked with AIC In</th>
        </thead>
        <tbody>
        </tbody>
    </table>
    <p class="info">Attachments</p>
    <table id="Attachments" class="InfoTable">
        <thead>
            <th>Title</th>
            <th id ="urlhead">URL</th>
            <th>Type</th>
        </thead>
        <tbody>
        </tbody>
    </table>
</div>
<!-- End Table -->
    </div>
<script>
    $(document).ready(function () {
        const urlParams = new URLSearchParams(window.location.search);
        var id = urlParams.get('UserId');
        console.log(id);
        GetUserDetails(id);
    });

    function GetUserDetails(id) {
        return $.Deferred(function ($dfd) {
            $.ajax({
                url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AICService.svc/GetAppliedForInternshipById?Id=' + id,
                method: 'GET',
                contentType: "application/json; charset=utf-8",
                async: false,
                headers: { "accept": "application/json;odata=verbose", "lang": "en" },
                success: function (data) {
                    data = JSON.parse(data.GetAppliedForInternshipByIdResult);
                    var result = JSON.parse(data.Records.Result);
                    console.log(result);

                    $("#InternshipInfo").append(`<tr>
                                                      <td> ${result.InternshipsViewModel.Title}</td>
                                                       <td>${result.InternshipsViewModel.ReferenceNumber} </td>
                                                       <td> ${result.InternshipsViewModel.Location}</td>
                                                        <td> ${new Date(result.InternshipsViewModel.ExpiryDate).toLocaleDateString()}</td>
                                                       </tr>`)


                    $("#GeneralInfo1").append(`<tr>
                                                      <td> ${result.FullName}</td>
                                                       <td>${result.Email} </td>
                                                       <td> ${result.MobileNumber}</td>
                                                        </tr>`)
                    $("#GeneralInfo2").append(`<tr>
                                                       <td> ${result.Address}</td>
                                                       <td>${new Date(result.BirthDate).toLocaleDateString()} </td>
                                                       <td>${new Date(result.StartDate).toLocaleDateString()} </td>
                                                       </tr>`)

                    result.AcademicDegrees.forEach(degree => {
                        var inProgress;
                        if (degree.InProgressBool == true)
                            inProgress = "Yes"
                        else
                            inProgress = "No"
                        $("#AcademicDegrees").append(`<tr>
                                                      <td> ${degree.DegreeLevel.TitleEn}</td>
                                                       <td>${new Date(degree.DegreeDate).toLocaleDateString()} </td>
                                                       <td> ${inProgress}</td>
                                                       <td> ${degree.University}</td>
                                                       <td>${degree.Specialization} </td>
                                                       </tr>`)
                    });

                    result.Certificates.forEach(Certificate => {
                        $("#Certificates").append(`<tr>
                                                      <td> ${Certificate.CertificateName}</td>
                                                       <td>${new Date(Certificate.CertifiedDate).toLocaleDateString()} </td>
                                                       <td> ${Certificate.CertifiedFrom}</td>
                                                       </tr>`)
                    });


                    if (result.TechnicalSkills.length > 0) {
                        result.TechnicalSkills.forEach(TechnicalSkill => {
                            $("#TechnicalSkills").append(`<tr>
                                                      <td> ${TechnicalSkill.SkillName}</td>
                                                       <td>${TechnicalSkill.YearsOfExperience} </td>
                                                       </tr>`)
                        });
                    }
                    else {
                        $("#TechnicalSkillsContainer").hide();
                    }
                    if ((result.LinkToPortfolio == null || result.LinkToPortfolio == "" || result.LinkToPortfolio == "null" || result.LinkToPortfolio == "undefined") &&
                        (result.JoinedUsAs == null || result.JoinedUsAs == "" || result.JoinedUsAs == "null") &&
                        (result.JoinedIn == null || result.JoinedIn == "" || result.JoinedIn == "null")){
                        $("#OtherInfo").hide();
                    }
                    else {
                        if (result.LinkToPortfolio == null || result.LinkToPortfolio == "null" || result.LinkToPortfolio == "undefined")
                            result.LinkToPortfolio = ""
                        if (result.JoinedUsAs == null || result.JoinedUsAs == "null")
                            result.JoinedUsAs = ""
                        if (result.JoinedIn == null || result.JoinedIn == "null")
                            result.JoinedIn = "";
                        $("#OtherInfo").append(`<tr>
                                                      <td><a href="${result.LinkToPortfolio}" target="_blank">${result.LinkToPortfolio}</a></td>
                                                       <td>${result.JoinedUsAs} </td>
                                                       <td> ${result.JoinedIn}</td>
                                                       </tr>`)
                        }
                    if (result.Documents.length > 0) {
                        result.Documents.forEach(Document => {
                            $("#Attachments").append(`<tr>
                                                      <td> ${Document.DisplayTitle}</td>
                                                       <td><a id="endDateValue" href="${Document.DocumentUrl}">${Document.DocumentUrl}</a></td>
                                                        <td>${Document.DocumenyType.DisplayTitle} </td>
                                                       </tr>`)
                        });
                    }
                    else {
                        $("#Attachments").hide();
                    }
                    console.log(result);
                    $dfd.resolve(data);
                },
                error: function () {
                    $dfd.reject();
                }
            });
        });
    }

    $('#Export').click(function (e) {
        e.preventDefault();
        exportPDF('contnet');
    });

    function exportPDF(id) {
        var links = [];
        for (let i = 0; i < $('[id*="docurl"]').length; i++) {
            links.push($('[id*="docurl"]')[i].href);
            $('[id*="docurl"]')[i].href = '';
            $('[id*="docurl"]')[i].innerHTML = '';
        }
        $('#urlhead')[0].innerHTML = '';
        const { jsPDF } = window.jspdf;

        var name = $('#GeneralInfo1 tr td')[0].innerText;
  
        var doc = new jsPDF('l', 'mm', [1000, 1150]);

        var pdfjs = document.getElementById(id);
        doc.html(pdfjs, {
            callback: function (doc) {
                doc.save(name + $.datepicker.formatDate('yymmdd', new Date()) + ".pdf");
                for (let i = 0; i < $('[id*="docurl"]').length; i++) {
                    $('[id*="docurl"]')[i].href = links[i];
                    $('[id*="docurl"]')[i].innerHTML = links[i];
                };
                $('#urlhead')[0].innerHTML = 'URL';
            },
            x: 12,
            y: 12
        });
    }
</script>
