using AIC.CrossCutting.Models;
using System.Collections.Generic;

namespace AIC.SP.Middleware.Models
{
    public class Query
    {
        public int PageSize { get; set; }
        [AllowedStringAttribute]
        public string SortingField { get; set; }
        public bool IsSortingAscending { get; set; }
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public string PagingInfo { get; set; }
        public string Lang { get; set; }

        [AllowedStringAttribute]
        public string WebUrl { get; set; }
    }

}
