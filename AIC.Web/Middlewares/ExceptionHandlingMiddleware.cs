using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AIC.CrossCutting.ExceptionHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AIC.Web.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger /* other dependencies */)
        {
            try
            {
                if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
                    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ExceptionHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.BadRequest; // 400 if unexpected
            string Message = "";
            if (ex is FirstLoginException)
            {
                code = HttpStatusCode.Redirect;
                Message = ex.Message;
            }
            else if (ex is UnAuthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
                Message = ex.Message;
            }
            else if (ex is UserLockedOutException)
            {
                code = HttpStatusCode.Locked;
                Message = ex.Message;
            }
            else if (ex is UserNotFoundException)
            {
                code = HttpStatusCode.NotFound;
                Message = ex.Message;
            }
            else if (ex is UserCredintialException)
            {
                code = HttpStatusCode.ExpectationFailed;
                Message = ex.Message;
            }
            else if (ex is UserForbiddenException)
            {
                code = HttpStatusCode.Forbidden;
                Message = ex.Message;
            }
            else if (ex is TokenExpiredException)
            {
                code = HttpStatusCode.BadGateway;
                Message = ex.Message;
            }
            else if (ex is UserRejectedException)
            {
                code = HttpStatusCode.UnavailableForLegalReasons;
                Message = ex.Message;
            }
            else if (ex is DuplicatePasswordException)
            {
                code = HttpStatusCode.Conflict;
                Message = ex.Message;
            }
            else if (ex is DuplicatedItemException)
            {
                code = HttpStatusCode.Conflict;
                Message = ex.Message;
            }
            else if (ex is InternalServerErrorException)
            {
                code = HttpStatusCode.InternalServerError;
                Message = ex.Message;
            }
            else if (ex is UserAlreadyRegisteredException)
            {
                code = HttpStatusCode.Conflict;
                Message = ex.Message;
            }
            else if(ex is UserAlreadyUnsubscribedException)
            {
                code = HttpStatusCode.Conflict;
                Message = ex.Message;
            }
            else if (ex is UserNotActivatedException)
            {
                code = HttpStatusCode.NotAcceptable;
                Message = ex.Message;
            }
            else if(ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
                Message = ex.Message;
            }
            else if (ex is PreConditionFailedException)
            {
                code = HttpStatusCode.PreconditionFailed;
                Message = ex.Message;
            }
            else if (ex is NothingChangedException)
            {
                code = HttpStatusCode.NotModified;
                Message = ex.Message;
            }
            else if (ex is FormURLNotValidException)
            {
                code = HttpStatusCode.NotAcceptable;
                Message = ex.Message;
            }
            else if (ex is ModelExpiredException)
            {
                code = HttpStatusCode.NotAcceptable;
                Message = ex.Message;
            }
            else if (ex is AppliedBeforeException)
            {
                code = HttpStatusCode.NotAcceptable;
                Message = ex.Message;
            }
            else if (ex is Exceptions)
                Message = ex.Message;
            else if (ex is AggregateException)
            {
                foreach (var innerEx in ((System.AggregateException)ex).InnerExceptions)
                {
                    if (innerEx is Exceptions)
                        Message += innerEx.Message;
                    else
                    {
                        code = HttpStatusCode.InternalServerError;
                        Message += innerEx.Message;
                    }
                }
            }
            else if (ex is Exception)
            {
                logger.LogError(ex.Message);
                Message = "خطأ في الخادم الداخلي";
            }
            var result = JsonConvert.SerializeObject(new
            {
                Success = "false",
                Message,
                ExceptionType = "Custom"
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            logger.LogError(Message);
            return context.Response.WriteAsync(result);
        }

    }
}
