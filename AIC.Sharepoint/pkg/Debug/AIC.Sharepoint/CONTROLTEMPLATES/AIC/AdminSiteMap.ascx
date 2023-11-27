<%@ Assembly Name="AIC.Sharepoint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6b7af8fe03873811" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminSiteMap.ascx.cs" Inherits="AIC.Sharepoint.CONTROLTEMPLATES.AIC.AdminSiteMap" %>


<link type="text/css" href="/_catalogs/masterpage/APA/CSS/bootstrap.min.css" rel="stylesheet"/>
<link type="text/css" href="/_catalogs/masterpage/APA/CSS/styles.css" rel="stylesheet"/>
<link type="text/css" href="/_catalogs/masterpage/APA/CSS/jquery-ui.css" rel="stylesheet"/>

<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/jquery-3.5.1.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/jquery-ui.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/jquery.jtable.min.js"></script>
<script type="text/javascript" src="/_catalogs/masterpage/APA/JS/vue.min.js"></script>

<link type="text/css" href="/_catalogs/masterpage/APA/CSS/AIC.css" rel="stylesheet"/>
 
<div class="sitemap" id="SiteMap" v-cloak>
    <div class="sitemap-details mt-5">
      <div class="row">
        <div class="col-lg-3 col-md-6 sitemap-column">
          <div class=""  v-for="item in siteMap">
            <a class="sitemap-title"  v-bind:href="'/'+item.Url" >{{item.Title}}</a>
          </div>
        </div>
        <div class="col-lg-3 col-md-6 sitemap-column" v-for="item in siteMap">
         
            <a class="sitemap-title" v-if="item.Url"  v-bind:href="'/'+item.Url">{{item.Title}}</a>
            <span class="sitemap-title" v-if="!item.Url">{{item.Title}}</span>
            <ul class="sitemap-list">
              <li v-for="nestedItem in item.element" >
                <a class="sitemap-title " v-bind:href="'/'+nestedItem.Url">{{nestedItem.Title}}</a>
              </li>
            </ul>         
        </div>
      </div>
    </div>
</div>


<script>
    var SiteMapVue = new Vue({
        el: '#SiteMap',
        data: {
            siteMap: [],
        },
        created: function () {
            this.getSiteMap();
        }, methods: {
            getSiteMap: function () {
                self = this;
                $.ajax({
                    url: _spPageContextInfo.webAbsoluteUrl + '/_vti_bin/AssetAIService.svc/SiteMap',
                    method: 'GET',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    headers: { "accept": "application/json;odata=verbose" },
                    success: function (data) {
                        self.siteMap = JSON.parse(data.SiteMapResult).element.element;
                    },
                    error: function () {
                        $dfd.reject();
                    }
                });
            }
        }
    });
</script>
