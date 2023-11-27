using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AIC.Data.ViewModels.Import
{
    public class ErrorsSheet
    {
        [Column("الرقم القومي")]
        public string NationalID { get; set; }
        [Column("الخطأ")]
        public string Error { get; set; }
    }
}
