using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.SPViewModels
{
    public class CareersViewModelAR : CareersBaseViewModel
    {
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string ReportsToAr { get; set; }
        public string JobOverviewAr { get; set; }
        public string DescriptionAr { get; set; }
        public string JobQualificationsAr { get; set; }
        public string JobRequirementsAr { get; set; }
    }
}
