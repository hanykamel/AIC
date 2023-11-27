using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Net.Http;

namespace AIC.Web.Filters
{
    public class InternalServerErrorFilter : Attribute, IExceptionFilter , IFilterMetadata
    {
        public void OnException(ExceptionContext context)
        {
                context.Result = new StatusCodeResult(500);
        }

      
    }
    public class LogAttribute : Attribute, IActionFilter, IFilterMetadata
    {
        public ILog _log;

        public LogAttribute()
        {
            _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        }

        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //  //  Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
        //}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.InternalServerError || context.HttpContext.Response.StatusCode == (int)HttpStatusCode.NotFound|| context.HttpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                _log.Error(context.HttpContext.Response.Body.ToString());
               // actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, "error 500");
            }
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}