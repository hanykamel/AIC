using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.ViewModels.Import
{
    public class ImportBulkUsersResult
    {
        public bool Success { get; set; }
        public bool EmptyFile { get; set; }
        public byte[] ResultFile { get; set; }
    }
}
