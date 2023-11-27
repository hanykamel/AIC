<%@ Assembly Name="AIC.Sharepoint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6b7af8fe03873811" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssetSubscribers.ascx.cs" Inherits="AIC.Sharepoint.CONTROLTEMPLATES.AIC.AssetSubscribers" %>
<%--<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/APA/CSS/bootstrap.min.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/APA/CSS/styles.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/APA/CSS/jquery-ui.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/APA/CSS/jtable_basic.min.css %&gt;" runat="server" After="corev15.css" />--%>
<link type="text/css" href="/_catalogs/masterpage/APA/CSS/bootstrap.min.css" rel="stylesheet"/>
<link type="text/css" href="/_catalogs/masterpage/APA/CSS/styles.css" rel="stylesheet"/>
<link type="text/css" href="/_catalogs/masterpage/APA/CSS/jquery-ui.css" rel="stylesheet"/>
<link type="text/css" href="/_catalogs/masterpage/APA/CSS/jtable_basic.min.css" rel="stylesheet"/>

<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/jquery-3.5.1.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/jquery-ui.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/jquery.jtable.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/vue.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/xlsx.full.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/FileSaver.min.js"></script>
<link type="text/css" href="/_catalogs/masterpage/APA/CSS/AIC.css" rel="stylesheet"/>
<div class="filtering">
    <form>
        Email: <input type="text" name="email" id="email" />
        <button type="submit" id="LoadRecordsButton">Search</button>
    </form>
 <button  onclick="ExportExcel()" >Export</button>
</div>
<div id="SubscribersTableContainer"></div>
<script>	
    $(document).ready(function () {
        $('#SubscribersTableContainer').jtable({
            title: 'Subscribers List',
            paging: true,
            pageSize: 20,
            sorting: true,
            defaultSorting: 'SubscriptionDate ASC',
            actions: {
                listAction: function (postData, jtParams) {
                    if (!postData) {
                        postData = JSON.stringify({
                            "pageIndex": jtParams.jtStartIndex,
                            "pageSize": 20
                        });
                    }
                    else {
                        postData = JSON.stringify({
                            "email": $('#email').val(),
                            "pageIndex": 0,
                            "pageSize": 20
                        });
                    }
                    return $.Deferred(function ($dfd) {
                        $.ajax({
                            url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AssetAIService.svc/GetSubscribersList',
                            data: postData,
                            method: 'POST',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            headers: { "accept": "application/json;odata=verbose" },
                            success: function (data) {
                                data = JSON.parse(data.GetSubscribersListResult);
                                var result = JSON.parse(data.Records).Result;
                                data.Records = result.Subscribers

                                data.TotalRecordCount = result.TotalRecordCount;
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
                    list: true
                },
                Email: {
                    title: 'Email',
                    sorting: true,
                },
                SubscriptionDate: {
                    title: 'Subscription Date',
                    sorting: true,
                    display: function (record) {
                        return record.record.SubscriptionDate;
                    }
                },
                InvalidEmail: {
                    title: 'Invalid Email',
                    sorting: false,
                    display: function (record) {
                        return record.record.InvalidEmail;
                    }
                }
            },

        });
        //Re-load records when user click 'load records' button.

        $('#SubscribersTableContainer').jtable('load');

        var postSearchData = JSON.stringify({
            "email": $('#email').val(),
            "pageIndex": 0,
            "pageSize": 20
        });
        $('#LoadRecordsButton').click(function (e) {
            e.preventDefault();
            $('#SubscribersTableContainer').jtable('load', postSearchData)
        });
    });
    function ExportExcel() {
        postData = JSON.stringify({
            "email": $('#email').val(),
            "pageIndex": 0,
            "pageSize": 10000
        });
        $.ajax({
            url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AssetAIService.svc/GetSubscribersList',
            data: postData,
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            async: false,
            headers: { "accept": "application/json;odata=verbose" },
            success: function (data) {
                data = JSON.parse(data.GetSubscribersListResult);
                var result = JSON.parse(data.Records).Result;
                data.Records = result.Subscribers
                ExportExcelFile(data.Records, "Subscribers")
            },
            error: function () {
            }
        });
    }
    function ExportExcelFile(data, filename) {
        /* this line is only needed if you are not adding a script tag reference */
        if (typeof XLSX == 'undefined') XLSX = new XLSX()/* require('xlsx')*/;
        /* make the worksheet */
        var ws = XLSX.utils.json_to_sheet(data);
        /* add to workbook */
        var wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, "Sheet");
        /* write workbook (use type 'binary') */
        var wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'binary' });
        /* generate a download */
        function s2ab(s) {
            var buf = new ArrayBuffer(s.length);
            var view = new Uint8Array(buf);
            for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
            return buf;
        }
        var Filename = filename + ".xlsx";
        saveAs(new Blob([s2ab(wbout)], { type: "application/octet-stream" }), Filename);
    }
</script>
