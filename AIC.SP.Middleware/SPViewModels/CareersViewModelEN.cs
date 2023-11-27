using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.SPViewModels
{
    public class CareersViewModelEN : CareersBaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReportsTo { get; set; }
        public string JobOverview { get; set; }
        public string Description { get; set; }
        public string JobQualifications { get; set; }
        public string JobRequirements { get; set; }

    }
}
