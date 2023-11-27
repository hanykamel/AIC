<%@ Page Language="C#" Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage,Microsoft.SharePoint.Publishing,Version=15.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" %>

<%@ Register TagPrefix="SharePointWebControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="PublishingNavigation" Namespace="Microsoft.SharePoint.Publishing.Navigation" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="AdminSiteMapCtrl" TagName="AdminSiteMap" Src="~/_controltemplates/16/APA/AdminSiteMap.ascx" %>   


<asp:Content contentplaceholderid="PlaceHolderPageTitle" runat="server">
    <SharePointWebControls:FieldValue ID="PageTitle" FieldName="Title" runat="server" />
</asp:Content>

<asp:Content contentplaceholderid="PlaceHolderMain" runat="server">

<WebPartPages:SPProxyWebPartManager runat="server" ID="spproxywebpartmanager"></WebPartPages:SPProxyWebPartManager>
    <AdminSiteMapCtrl:AdminSiteMap runat="server" id="SiteMap"/>   
<WebPartPages:WebPartZone ID="ContentZone" runat="server" Title="ContentZone"><ZoneTemplate>

</ZoneTemplate></WebPartPages:WebPartZone>
</asp:Content>
