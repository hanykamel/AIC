using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace AIC.Web.Filters
{
    public class MemoryCacheFilter: Attribute, IActionFilter
    {
        public string CacheKey { get; set; }
        public Type ResultType { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var _memoryCache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache));
            var caches = Activator.CreateInstance(ResultType);
            var isExist = ((IMemoryCache)_memoryCache).TryGetValue(CacheKey, out caches);
            if (isExist)
                context.Result = new OkObjectResult(caches);

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var _memoryCache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache));

            ((IMemoryCache)_memoryCache).Set(CacheKey, ((OkObjectResult)context.Result).Value);

        }
    }
}
