using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.ViewModels.Import
{
    public class AddBulkResult
    {
        public int Total { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
    }
}
