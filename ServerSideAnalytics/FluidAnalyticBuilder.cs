using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerSideAnalytics
{
    public class FluidAnalyticBuilder
    {
        private readonly IAnalyticStore _store;
        private List<Func<HttpContext, bool>> _exclude;
        private string[] _allowedCategories;
        private string[] _queryParams;
        private string _cat;
        private string _key;
        public IConfiguration Configuration { get; }

        internal FluidAnalyticBuilder(IAnalyticStore store, IConfiguration configuration)
        {
            _store = store;
            Configuration = configuration;
            _allowedCategories = Configuration["AllowedCategories"].Split(',');
            _queryParams = Configuration["SearchedQueryParams"].ToLower().Split(',');
        }

        internal async Task Run(HttpContext context, Func<Task> next)
        {
            var identity = context.UserIdentity();

            //Pass the command to the next task in the pipeline
            await next.Invoke();

            //This request should be filtered out ?
            if (_exclude?.Any(x => x(context)) ?? false)
            {
                return;
            }
            _cat = Array.Find(_allowedCategories, cat => context.Request.Path.Value.Contains(cat));
            _key = context.Request.Query.FirstOrDefault(p => _queryParams.Contains(p.Key.ToLower())).Value;

            if (!String.IsNullOrEmpty(_cat) && !String.IsNullOrEmpty(_key))
            {
                //Debug.WriteLine(context.Request.Query.Where(x => x.Key.ToLower() == "id" || x.Key.ToLower() == "albumname").FirstOrDefault().Value.ToString());
                //var x = context.Request.QueryString.Value ;
                var req = new WebRequest
                {
                    Timestamp = DateTime.Now,
                    Identity = identity,
                    RemoteIpAddress = context.Connection.RemoteIpAddress,
                    Method = context.Request.Method,
                    UserAgent = context.Request.Headers["User-Agent"],
                    Path = context.Request.Path.Value,
                    IsWebSocket = context.WebSockets.IsWebSocketRequest,
                    //Ask the store to resolve the geo code of gived ip address 
                    CountryCode = context.Request.Headers["lang"]
                };
                req.Category = _cat;
                if (context.Request.Path.Value.LastIndexOf('/') != 0)
                {
                    //req.Key = context.Request.Path.Value.Substring(context.Request.Path.Value.LastIndexOf('/') + 1);
                    /*string key = context.Request.Query.Where(x => x.Key.ToLower() == "id" || x.Key.ToLower() == "albumname").FirstOrDefault().Value;
                    key = context.Request.Query.FirstOrDefault(p => _queryParams.Contains(p.Key.ToLower())).Value;
                    Debug.WriteLine(key);*/
                    req.Key = _key;

                }
                await _store.StoreWebRequestAsync(req);

            }
            
        }
        public FluidAnalyticBuilder Exclude(Func<HttpContext, bool> filter)
        {
            if (_exclude == null) _exclude = new List<Func<HttpContext, bool>>();
            _exclude.Add(filter);
            return this;
        }

        public FluidAnalyticBuilder Exclude(IPAddress ip) => Exclude(x => Equals(x.Connection.RemoteIpAddress, ip));

        public FluidAnalyticBuilder LimitToPath(string path) => Exclude(x => !Equals(x.Request.Path.StartsWithSegments(path)));

        public FluidAnalyticBuilder ExcludePath(params string[] paths)
        {
            return Exclude(x => paths.Any(path => x.Request.Path.StartsWithSegments(path)));
        }

        public FluidAnalyticBuilder ExcludeExtension(params string[] extensions)
        {
            return Exclude(x => extensions.Any(ext => x.Request.Path.Value.EndsWith(ext)));
        }

        public FluidAnalyticBuilder ExcludeLoopBack() => Exclude(x => IPAddress.IsLoopback(x.Connection.RemoteIpAddress));

        public FluidAnalyticBuilder ExcludeIp(IPAddress address) => Exclude(x => x.Connection.RemoteIpAddress.Equals(address));

        public FluidAnalyticBuilder ExcludeStatusCodes(params HttpStatusCode[] codes) => Exclude(context => codes.Contains((HttpStatusCode)context.Response.StatusCode));

        public FluidAnalyticBuilder ExcludeStatusCodes(params int[] codes) => Exclude(context => codes.Contains(context.Response.StatusCode));

        public FluidAnalyticBuilder LimitToStatusCodes(params HttpStatusCode[] codes) => Exclude(context => !codes.Contains((HttpStatusCode)context.Response.StatusCode));

        public FluidAnalyticBuilder LimitToStatusCodes(params int[] codes) => Exclude(context => !codes.Contains(context.Response.StatusCode));
    }
}
