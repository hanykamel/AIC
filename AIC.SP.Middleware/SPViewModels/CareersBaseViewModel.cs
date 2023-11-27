using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.SPViewModels
{
    public class CareersBaseViewModel
    {
        public string ReferenceNumber { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string JobType { get; set; }
        public string Location { get; set; }
        public DateTime VacancyExpiryDate { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ListName { get; set; }


    }
}
