<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAppliedToCareerDetailsUserControl.ascx.cs" Inherits="AIC.Sharepoint.Webparts.UserAppliedToCareerDetails.UserAppliedToCareerDetailsUserControl" %>

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
<div id="contnet" style="width:100%">
<div id="JobInfoContainer" class="contain" style="background-color: #e5e4e4;">
    <p class="info">Vacancy Info</p>
    <table id="JobInfo" class="InfoTable">
        <thead>
            <th>Title</th>
            <th>Vacancy Reference Number</th>
            <th>Type</th>
            <th>Location</th>
            <th>Vacancy Expiry Date</th>
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
            <th>Full name</th>
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
<div id="WorkExperienceContainer" class="contain">
    <p class="info">Work Experience</p>
    <table id="WorkExperience" class="InfoTable">
        <thead>
            <th>Current/Last Occupation</th>
            <th>Current/Last Employer</th>
            <th>Job Type</th>
            <th>Start Date</th>
            <th id="endDate">End Date</th>
            <th>Current Job</th>
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
            <th>Link To Portfolio</th>
            <th>Worked With AIC As</th>
            <th>Worked With AIC In</th>
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
                url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AICService.svc/GetAppliedForCareerById?Id=' + id,
                method: 'GET',
                contentType: "application/json; charset=utf-8",
                async: false,
                headers: { "accept": "application/json;odata=verbose", "lang": "en" },
                success: function (data) {
                    data = JSON.parse(data.GetAppliedForCareerByIdResult);
                    var result = JSON.parse(data.Records.Result);
                    console.log(result);

                    $("#JobInfo").append(`<tr>
                                                      <td> ${result.CareersViewModel.Title}</td>
                                                       <td>${result.CareersViewModel.ReferenceNumber} </td>
                                                       <td> ${result.CareersViewModel.JobType}</td>
                                                       <td> ${result.CareersViewModel.Location}</td>
                                                        <td>  ${new Date(result.CareersViewModel.VacancyExpiryDate).toLocaleDateString()}</td>
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
                                                       <td>${new Date(degree.DegreeDate).toLocaleDateString()}</td>
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

                    if (result.WorkExperiences.length > 0) {
                        result.WorkExperiences.forEach(WorkExperience => {
                            var jopTypeTitle = "", currentJob = "", endDate;
                            if (WorkExperience.JobType != null)
                                jopTypeTitle = WorkExperience.JobType.TitleEn;
                            if (WorkExperience.CurrentJobBool == true) {
                                currentJob = "Yes";
                                endDate = "";

                            }
                            else {
                                currentJob = "No";
                                endDate = new Date(WorkExperience.EndDate).toLocaleDateString();

                            }


                            $("#WorkExperience").append(`<tr>
                                                       <td> ${WorkExperience.Job}</td>
                                                       <td> ${WorkExperience.Company} </td>
                                                       <td> ${jopTypeTitle}</td>
                                                       <td> ${new Date(WorkExperience.StartDate).toLocaleDateString()}</td>
                                                       <td id="endDateValue"> ${endDate} </td>
                                                       <td> ${currentJob}</td>
                                                       </tr>`)
                            if (WorkExperience.CurrentJobBool == true) {
                                $("#endDate").hide();
                                $("#endDateValue").hide();
                            }
                            else {
                                $("#endDate").show();
                                $("#endDateValue").show();

                            }
                        });
                    }
                    else {
                        $("#WorkExperienceContainer").hide();
                    }

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
                        (result.JoinedUsAs == null || result.JoinedUsAs == "null" || result.JoinedUsAs == "") &&
                        (result.JoinedIn == null || result.JoinedIn == "null" || result.JoinedIn == "")) {
                        $("#OtherInfo").hide();
                    }
                    else {
                        if (result.LinkToPortfolio == null || result.LinkToPortfolio == "" || result.LinkToPortfolio == "null" || result.LinkToPortfolio == "undefined")
                            result.LinkToPortfolio = ""
                        if (result.JoinedUsAs == null || result.JoinedUsAs == "null")
                            result.JoinedUsAs = ""
                        if (result.JoinedIn == null || result.JoinedIn == "null")
                            result.JoinedIn = "";
                        $("#OtherInfo").append(`<tr>
                                                      <td> <a href="${result.LinkToPortfolio}" target="_blank">${result.LinkToPortfolio}</a></td>
                                                       <td>${result.JoinedUsAs} </td>
                                                       <td> ${result.JoinedIn}</td>
                                                       </tr>`)
                    }

                    if (result.Documents.length > 0) {
                        result.Documents.forEach(Document => {
                            $("#Attachments").append(`<tr>
                                                      <td> ${Document.DisplayTitle}</td>
                                                       <td><a id="docurl" href="${Document.DocumentUrl}">${Document.DocumentUrl}</a></td>
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
