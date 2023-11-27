using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware
{
    public class CacheListViewModel
    {
        public List<CacheViewModel> Records { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
