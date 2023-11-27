using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class AppliedForInternshipsViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string ReferenceNumber { get; set; }
        public string InternshipTitleAr { get; set; }
        public string InternshipTitleEn { get; set; }
        public DateTime AppliedDate { get; set; }
        public string FullName { get; set; }
        public string CVURL { get; set; }
    }

    public class AppliedForInternshipsListViewModel
    {
        public IEnumerable<AppliedForInternshipsViewModel> List { get; set; }
        public int TotalCount { get; set; }
    }
}
