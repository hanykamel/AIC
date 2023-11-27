
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware.Interfaces
{
    public interface ISearchService
    {
        public CacheListViewModel Search(string lang, string key, string category, DateTime? from, DateTime? to, int pageIndex = 0, int pageSize = 9);
      
    }
}
