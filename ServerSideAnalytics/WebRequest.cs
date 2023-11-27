using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ServerSideAnalytics
{
    public class WebRequest
    {
        public DateTime Timestamp { get; set; }

        public string Identity { get; set; }

        public IPAddress RemoteIpAddress { get; set; }

        public string Method { get; set; }

        public string Path { get; set; }

        public string UserAgent { get; set; }

        public string Referer { get; set; }

        public bool IsWebSocket { get; set; }
        public string CountryCode { get; set; }
        public string Category { get; set; }
        public string Key { get; set; }
    }
}
