using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Configuration;

namespace AIC.Web.Filters
{ 
    public class InternalApiFilter : Attribute, IActionFilter, IFilterMetadata
    {
        string Key = "SecretKey"; 
        string Value = ConfigurationManager.AppSettings["headerValue"];
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public  void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ActionArguments == null || string.IsNullOrWhiteSpace(actionContext.HttpContext.Request.Headers[Key]))
            {
                log.Error("Invalid API");
                actionContext.Result = new ForbidResult("Invalid API");
            }

            if (!string.IsNullOrWhiteSpace(actionContext.HttpContext.Request.Headers[Key]))
            {
                var secretValue = actionContext.HttpContext.Request.Headers.TryGetValue(Key,out var headerVal);
                if (!secretValue  || string.IsNullOrWhiteSpace(headerVal) || headerVal != Value)
                {
                    log.Error("Invalid API");
                    actionContext.Result = new ForbidResult("Invalid API");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}