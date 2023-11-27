using AIC.SP.Middleware.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AIC.SP.Middleware.Services
{
    public class SiteMapService : ISitemapService
    {
        private readonly IConfiguration _config;

        public SiteMapService(IConfiguration config)
        {
            _config = config;
        }
        public string GetSiteMap(string lang)
        {
            string URLString = _config.GetSection("Sharepoint:SitemapUrl").Value.Replace("{lang}", lang);

            XmlDocument doc = new XmlDocument();
            doc.Load(URLString);
            string json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }
    }
}
