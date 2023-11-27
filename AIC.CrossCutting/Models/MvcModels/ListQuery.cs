using AIC.SP.Middleware.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.Models.MvcModels
{
    public class ListQuery
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public string SortingField { get; set; }
        public bool IsSortingAscending { get; set; }
        public List<ListFilter> Filters { get; set; }
    }
}
