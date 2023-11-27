using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class InternshipViewModelDB
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string ProjectDepartment { get; set; }
        public string InternshipOverview { get; set; }
        public string Description { get; set; }
        public string InternshipQualifications { get; set; }
        public string InternshipRequirements { get; set; }
        public string Location { get; set; }
        public string LocationAr { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
