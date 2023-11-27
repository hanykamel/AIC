
using AIC.SP.Middleware.SPViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Asset.SharePoint.Middleware.Helpers
{
    public static class SharepointFieldHelpers
    {
        public static IConfiguration configurations;
        public static string KeyWordsQuery(string keyWords)
        {
            string[] words = keyWords.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 1)
            {
                string query = string.Concat(Enumerable.Repeat("<Or>", words.Length - 1));
                query += $@"<Contains><FieldRef Name='Keywords' /><Value Type='Text'>{words[0]}</Value></Contains>";
                for (int i = 1; i < words.Length; i++)
                {
                    query += $@"<Contains><FieldRef Name='Keywords' /><Value Type='Text'>{words[i]}</Value></Contains></Or>";
                }
                return query;
            }
            else
            {
                return $@"<Contains><FieldRef Name='Keywords' /><Value Type='Text'>{words[0]}</Value></Contains>";
            }
        }
        public static DateTime ConvertDateFromSharepoint(DateTime dateTime)
        {
            return System.TimeZone.CurrentTimeZone.ToLocalTime(dateTime);
        }
        public static DateTime? ConvertDateFromSharepoint(ListItem item, string fieldName)
        {
            if (item[fieldName] != null)
            {
                return System.TimeZone.CurrentTimeZone.ToLocalTime(Convert.ToDateTime(Convert.ToString(item[fieldName])));
            }
            return null;
        }
        public static string GetAttachedFileURL(string urlTag)
        {
            try
            {
                var SharepointSection = configurations.GetSection("Sharepoint");
                string hostUrl = SharepointSection.GetSection("HostURL").Value;
                return String.IsNullOrEmpty(urlTag) ? null : hostUrl + urlTag.Substring(1);
            }
            catch
            {
                return "#";
            }
        }
        public static string getHostURL()
        {
            var SharepointSection = configurations.GetSection("Sharepoint");
            return  SharepointSection.GetSection("HostURL").Value;
        }
        public static string GetPublishingPageImageURL(string ImgTag)
        {
            try
            {
                return String.IsNullOrEmpty(ImgTag) ? "#" : ImgTag.Contains("http://") || ImgTag.Contains("https://") ? 
                    XElement.Parse(ImgTag + "</img>").Attribute("src").Value : getHostURL() + XElement.Parse(ImgTag + "</img>").Attribute("src").Value;
            }
            catch
            {
                return "#";
            }
        }
        public static string GetURLFieldValue(ListItem listItem, string fieldName)
        {
            var SharepointSection = configurations.GetSection("Sharepoint");
            string siteURL = SharepointSection.GetSection("SiteURL").Value;
            string hostURL = getHostURL();
            FieldUrlValue URL = listItem[fieldName] as FieldUrlValue;
            return URL != null ? URL.Url.Replace(siteURL, hostURL + "/").ToString() : "";
        }
        public static string GetURLFieldDescription(ListItem listItem, string fieldName)
        {
            var SharepointSection = configurations.GetSection("Sharepoint");
            string siteURL = SharepointSection.GetSection("SiteURL").Value;
            string hostURL = getHostURL();
            FieldUrlValue URL = listItem[fieldName] as FieldUrlValue;
            return URL != null ? URL.Description : "";
        }
        public static string getLookupFieldValue(ListItem listItem, string lookupFieldName)
        {
            FieldLookupValue lookup = listItem[lookupFieldName] as FieldLookupValue;
            string lvalue = lookup.LookupValue;
            return lvalue;
        }

        public static List<LookupViewModel> getMultiLookupField(ListItem listItem, string lookupFieldName)
        {
            var lookups = (FieldLookupValue[])listItem[lookupFieldName];
            List<LookupViewModel> lookupValues = new List<LookupViewModel>();
            foreach (FieldLookupValue lookup in lookups)
            {
                lookupValues.Add(new LookupViewModel { Id = lookup.LookupId, Value = lookup.LookupValue });
            }
            return lookupValues;
        }
        public static int getLookupFieldId(ListItem listItem, string lookupFieldName)
        {
            FieldLookupValue lookup = listItem[lookupFieldName] as FieldLookupValue;
            int lId = lookup.LookupId;
            return lId;
        }
        public static string GetRelativeURLFieldValue(ListItem listItem, string fieldName)
        {
            var SharepointSection = configurations.GetSection("Sharepoint");
            string siteUrl = SharepointSection.GetSection("SiteURL").Value;
            FieldUrlValue URL = listItem[fieldName] as FieldUrlValue;
            return URL.Url.Replace(siteUrl, "/").ToString();
        }
        public static string PublicURL(string URL)
        {
            if (!string.IsNullOrWhiteSpace(URL))
            {
            var SharepointSection = configurations.GetSection("Sharepoint");
                string siteURL = SharepointSection.GetSection("SiteURL").Value;
                string hostURL = getHostURL();
                URL = URL.Contains(siteURL) ? URL.Replace(siteURL, hostURL) : hostURL + URL;
                return URL;
            }
            return URL;
        }
        public static string DownlodableFileUrl(string URL)
        {
            if (URL.StartsWith("/"))
                URL = URL.Remove(0, 1);
            string publicUrl = PublicURL(URL);
            return HttpUtility.UrlPathEncode(/*getHostURL() + "_layouts/download.aspx?SourceUrl=" +*/ publicUrl);

       // http://10.107.167.210:1111/ar/_layouts/download.aspx?SourceUrl=http://10.107.167.210:1111/ar/Resources/Egypt%20National%20AI%20Strategy%20AR(11-4-2021)01.pdf
        }
       
    }
}
