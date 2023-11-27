
using AIC.SP.Middleware.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace AIC.Web.Filters
{
    public class LanguageFilter : Attribute, IActionFilter, IFilterMetadata
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
       {
            bool langSuccess = context.HttpContext.Request.Headers.TryGetValue("lang",out var lang);


            if (lang.ToString().ToLower() != "ar" && lang.ToString().ToLower() != "en")
            {
                log.Error("Error in Language in URL");
                context.Result = new BadRequestObjectResult("Arabic Or English Sites Only");
            }

            var query = context.ActionArguments.Select(c=>c.Value).OfType<Query>().FirstOrDefault();
            if (query != null)
            {
                query.Lang = lang;
                bool headerValSucceess = context.HttpContext.Request.Headers.TryGetValue("pagingInfo",out var headerVal);
                if (headerValSucceess && !string.IsNullOrEmpty(headerVal))
                    query.PagingInfo = headerVal;
            }
        }

        
    }
    //public class CustomAuthorization : Attribute, IAuthorizationFilter, IFilterMetadata
    //{
    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    //    {
    //        actionContext.Response = new HttpResponseMessage
    //        {
    //            StatusCode = HttpStatusCode.Unauthorized,
    //            Content = new StringContent("You are unauthorized to access this resource")
    //        };
    //    }
    //}
}