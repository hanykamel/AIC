using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class CareerViewModelDB
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string JobType { get; set; }
        public string JobTypeAr { get; set; }
        public string ReportsTo { get; set; }
        public string JobOverview { get; set; }
        public string Description { get; set; }
        public string JobQualifications { get; set; }
        public string JobRequirements { get; set; }
        public string Location { get; set; }
        public string LocationAr { get; set; }
        public DateTime VacancyExpiryDate { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
