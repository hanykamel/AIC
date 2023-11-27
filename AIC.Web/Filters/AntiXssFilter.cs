


using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AIC.Web.Filters
{
    public class AntiXssFilter : Attribute, IActionFilter, IFilterMetadata
    {


        //  private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                // Check XSS in URL
                if (!string.IsNullOrWhiteSpace(context.HttpContext.Request.GetEncodedUrl()))
                {
                    var url = context.HttpContext.Request.GetEncodedUrl();

                    if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
                    {
                        context.Result = new ForbidResult("Xss Found in Request");
                        return;
                    }
                }

                //// Check XSS in query string
                if (!string.IsNullOrWhiteSpace(context.HttpContext.Request.QueryString.Value))
                {
                    var queryString = WebUtility.UrlDecode(context.HttpContext.Request.QueryString.Value);

                    if (CrossSiteScriptingValidation.IsDangerousString(queryString, out _))
                    {
                        context.Result = new ForbidResult("Xss Found in Request");
                        return;
                    }
                }

                // Check XSS in request content
                if (context.HttpContext.Request.Method.ToString().ToLower() == "post")
                {
                    var content = ReadRequestBody(context);
                    if (CrossSiteScriptingValidation.IsDangerousString(content, out _))
                    {
                        new ForbidResult("Xss Found in Request");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                context.Result = new ForbidResult(ex.Message);
            }
        }


        private string ReadRequestBody(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContentLength > 0)
            {
                using (MemoryStream buffer = new MemoryStream())
                {
                    do
                    {
                        context.HttpContext.Request.Body.CopyToAsync(buffer).Wait();
                    } while (buffer.Length == 0);
                    buffer.Position = 0;
                    var encoding = Encoding.UTF8;
                    string requestContent = new StreamReader(buffer, encoding).ReadToEndAsync().Result;
                    return requestContent;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        
    }

    public static class CrossSiteScriptingValidation
    {
        private static readonly char[] StartingChars = { '<', '>'};

        #region Public methods

        public static bool IsDangerousString(string s, out int matchIndex)
        {
            //bool inComment = false;
            matchIndex = 0;

            for (var i = 0; ;)
            {

                // Look for the start of one of our patterns 
                var n = s.IndexOfAny(StartingChars, i);

                // If not found, the string is safe
                if (n < 0) return false;

                // If it's the last char, it's safe 
                if (n == s.Length - 1) return false;

                matchIndex = n;

                switch (s[n])
                {
                    case '<':
                        // If the < is followed by a letter or '!', it's unsafe (looks like a tag or HTML comment)
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?') return true;
                        break;
                    case '&':
                        // If the & is followed by a #, it's unsafe (e.g. S) 
                        if (s[n + 1] == '#') return true;
                        break;

                }

                // Continue searching
                i = n + 1;
            }
        }

        #endregion

        #region Private methods

        private static bool IsAtoZ(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        #endregion


        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
        public static string ToJSON(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}