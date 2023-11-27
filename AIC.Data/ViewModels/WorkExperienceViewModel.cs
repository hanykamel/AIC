using AIC.Data.ViewModels.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class WorkExperienceViewModel
    {
        public string Job { get; set; }
        public string Company { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<string> CurrentJob { get; set; }
        public bool? CurrentJobBool { get; set; }
        public int? JobTypeId { get; set; }
        public LookupsViewModel JobType { get; set; }

    }
}
