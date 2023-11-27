<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscribersListUserControl.ascx.cs" Inherits="AIC.Sharepoint.Webparts.SubscribersList.SubscribersListUserControl" %>

<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/bootstrap.min.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/styles.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/jquery-ui.css %&gt;" runat="server" After="corev15.css" />
<SharePoint:CssRegistration Name="&lt;% $SPUrl:~siteCollection/_catalogs/masterpage/AIC/CSS/jtable_basic.min.css %&gt;" runat="server" After="corev15.css" />

<div class="filtering">
    <form>
        Email: <input type="text" name="email" id="email" />
        <button type="submit" id="SearchButton">Search</button>
        <button type="submit" id="btnExport">Export</button>
    </form>
</div>
<div id="SubscribersTableContainer"></div>
<script>	
    $(document).ready(function () {
        $('#SubscribersTableContainer').jtable({
            title: '',
            paging: true,
            pageSize: 20,
            sorting: true,
            defaultSorting: 'SubscriptionDate ASC',
            actions: {
                listAction: function (postData, jtParams) {
                    if (!postData) {
                        postData = "?PageSize=" + jtParams.jtPageSize + "&PageIndex= " + jtParams.jtStartIndex / jtParams.jtPageSize;
                    }
                    else {
                        postData = "?PageSize=" + jtParams.jtPageSize + "&PageIndex=" + jtParams.jtStartIndex / jtParams.jtPageSize + "&Email=" + $('#email').val();
                    }
                    return $.Deferred(function ($dfd) {
                        $.ajax({
                            url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AICService.svc/GetSubscribersList' + postData,
                            method: 'GET',
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            headers: { "accept": "application/json;odata=verbose" },
                            success: function (data) {
                                data = JSON.parse(data.GetSubscribersListResult);
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
                Email: {
                    title: 'Email',
                    sorting: true,
                },
                SubscriptionDate: {
                    title: 'Subscription Date',
                    sorting: true,
                    type: Date,
                    displayFormat: 'dd/mm/yy',
                    display: function (record) {
                        return record.record.SubscriptionDate.split('T')[0];
                    }
                }
            },

        });
        //Re-load records when user click 'load records' button.

        $('#SubscribersTableContainer').jtable('load');

        var postSearchData = "?PageSize=20&PageIndex=0&Email=" + $('#email').val();
        $('#SearchButton').click(function (e) {
            e.preventDefault();
            $('#SubscribersTableContainer').jtable('load', postSearchData)});
    });

    $('#email').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var SearchData = "?PageSize=20&PageIndex=0&Email=" + $('#email').val();
            e.preventDefault();
            $('#SubscribersTableContainer').jtable('load', SearchData)
        }
    });

    $("#btnExport").click(function (e) {
        window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#SubscribersTableContainer').html()));
        e.preventDefault();
    });
</script>