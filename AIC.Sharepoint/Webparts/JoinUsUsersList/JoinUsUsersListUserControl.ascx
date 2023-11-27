<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JoinUsUsersListUserControl.ascx.cs" Inherits="AIC.Sharepoint.Webparts.JoinUsUsersList.JoinUsUsersListUserControl" %>


<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/bootstrap.min.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/styles.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/jquery-ui.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/jtable_basic.min.css %&gt;" runat="server" After="corev15.css" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip-utils/0.1.0/jszip-utils.js" type="text/javascript"> </script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.5.0/jszip.min.js" type="text/javascript"> </script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/1.3.8/FileSaver.js"></script>

<style>
    .textbox {
        margin-right: 15px;
        cursor: pointer;
    }
    .label{
        font-weight: bold;
        margin-right: 10px;
    }
     .button{
        font-weight: bold;
        margin-right: 10px;
    }
</style>

<div class="filtering"><form>
        <label class="label"> Email: </label><input class="textbox" type="text" name="email" id="email" />
        <label class="label">Universty: </label><input class="textbox" type="text" name="Universty" id="Universty" />
        <label class="label">Age: </label><input class="textbox" type="text" name="Universty" id="Age" />
        <br>
        <br>
        <label class="label">Applied Date from: </label><input class="textbox" type="date" name="date" id="dateFrom" />
        <label class="label">Applied Date To: </label><input class="textbox" type="date" name="date" id="dateTo" />
        <br>
        <br><button class="button" type="submit" id="LoadRecordsButton">Search</button>
        <button class="button" type="submit" id="btnExport">Download All CVs</button>
        <br>
        <br>
    </form>
</div>
<div id="AppliedTableContainer"></div>
<script>	
    var CVsURLs = [];
    $(document).ready(function () {
        $('#AppliedTableContainer').jtable({
            title: '',
            paging: true,
            pageSize: 20,
            sorting: true,
            defaultSorting: 'AppliedDate ASC',
            actions: {
                listAction: function (postData, jtParams) {
                    if (!postData) {
                        postData = "?PageSize=" + jtParams.jtPageSize + "&PageIndex= " + jtParams.jtStartIndex / jtParams.jtPageSize;
                    }
                    else {
                        postData = "?PageSize=" + jtParams.jtPageSize + "&PageIndex=" + jtParams.jtStartIndex / jtParams.jtPageSize
                            + "&Email=" + $('#email').val() + "&University=" + $('#Universty').val()
                            + "&Age=" + $('#Age').val() + "&AppliedDateFrom=" + $('#dateFrom').val()
                            + "&AppliedDateTo=" + $('#dateTo').val() ;
                    }
                    return $.Deferred(function ($dfd) {
                        $.ajax({
                            url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AICService.svc/GetJoinUsList' + postData,
                            method: 'GET',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            headers: { "accept": "application/json;odata=verbose" },
                            success: function (data) {
                                CVsURLs = [];
                                data = JSON.parse(data.GetJoinUsListResult);
                                var result = JSON.parse(data.Records.Result);
                                data.Records = result.List
                                data.TotalRecordCount = result.TotalCount;
                                console.log(data);
                                $dfd.resolve(data);
                            },
                            error: function () {
                                $dfd.reject();
                            }
                        });
                    });
                }
            },
            fields: {
                Id: {
                    title: 'Id',
                    key: true,
                    list: false
                },
                FullName: {
                    title: 'Name',
                    sorting: true,
                } ,
                Email: {
                    title: 'Email',
                    sorting: true,
                },
                SubscriptionDate: {
                    title: 'Apply Date',
                    sorting: true,
                    display: function (record) {
                        return record.record.AppliedDate.split('T')[0];
                    }
                },
                ViewButton: {
                    title: 'View Details',
                    list: true,
                    display: function (record) {
                        CVsURLs.push(record.record.CVURL);
                        return '<a href="/Pages/JoinUsUserDetails.aspx?UserId=' + record.record.Id+ '">View Details</a>';
                    }
                },
                DeleteButton: {
                    title: 'Delete',
                    list: true,
                    display: function (record) {
                        return "<a onclick='DeleteUser(\"" + record.record.Id + "\"); return false;' href='#'>Delete</a>";
                    }
                },
                DownloadButton: {
                    title: 'Download CV',
                    list: true,
                    display: function (record) {
                        if (record.record.CVURL == null || record.record.CVURL == "")
                            return "";
                        else
                            return '<a href="' + record.record.CVURL + '">Download CV</a>';
                    }
                }
            }
        });
        //Re-load records when user click 'load records' button.

        $('#AppliedTableContainer').jtable('load');

        var postSearchData = "?PageSize=20&PageIndex=0&Email=" + $('#email').val()
            + "&University=" + $('#Universty').val() + "&Age=" + $('#Age').val() + "&AppliedDateFrom="
            + $('#dateFrom').val() + "&AppliedDateTo=" + $('#dateTo').val();
        $('#LoadRecordsButton').click(function (e) {
            e.preventDefault();
            $('#AppliedTableContainer').jtable('load', postSearchData)});
    });

    $('#email').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var SearchData = "?PageSize=20&PageIndex=0&Email=" + $('#email').val()
                + "&University=" + $('#Universty').val() + "&Age=" + $('#Age').val()
                + "&AppliedDateFrom=" + $('#dateFrom').val()
                + "&AppliedDateTo=" + $('#dateTo').val();
            e.preventDefault();
            $('#AppliedTableContainer').jtable('load', SearchData)
        }
    });

    function DeleteUser(id) {
        let isExecuted = confirm("Are you sure you want to delete this application?");

        if (isExecuted) {
            return $.Deferred(function ($dfd) {
                $.ajax({
                    url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AICService.svc/DeleteAppliedJoinUsById?Id=' + id,
                    method: 'GET',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    headers: { "accept": "application/json;odata=verbose", "lang": "en" },
                    success: function (data) {
                        data = JSON.parse(data.DeleteAppliedJoinUsByIdResult);

                        console.log(data);
                        $dfd.resolve(data);
                        $('#AppliedTableContainer').jtable('load');
                    },
                    error: function () {
                        $dfd.reject();
                    }
                });
            });
        }
    }

    //$("#btnExport").click(function (e) {
    //    window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#AppliedTableContainer').html()));
    //    e.preventDefault();
    //});

    $("#btnExport").click(function (e) {
        e.preventDefault();
        DownloadAllAsZip()
    });

    function DownloadAllAsZip() {
        var urls = CVsURLs;
        var currentdate = new Date();
        var datetime = currentdate.getDate().toString() + currentdate.getMonth().toString()
            + currentdate.getFullYear().toString() + currentdate.getHours().toString() + currentdate.getMinutes().toString();
        var nombre = "CVs_" + datetime.toString();
        //The function is called
        compressed_img(urls, nombre);

    }
    function compressed_img(urls, nombre) {
        var zip = new JSZip();
        var count = 0;
        var name = nombre + ".zip";
        urls.forEach(function (url) {
            JSZipUtils.getBinaryContent(url, function (err, data) {
                if (err) {
                    console.log(err.message);
                }
                zip.file(url, data, {
                    binary: true
                });
                count++;
                if (count == urls.length) {
                    zip.generateAsync({
                        type: 'blob'
                    }).then(function (content) {
                        saveAs(content, name);
                    });
                }
            });
        });
    }

</script>