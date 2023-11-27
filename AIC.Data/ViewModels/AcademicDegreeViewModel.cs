using AIC.Data.Lookups;
using AIC.Data.ViewModels.Lookups;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class AcademicDegreeViewModel
    {
        public int DegreeLevelId { get; set; }
        public LookupsViewModel DegreeLevel { get; set; }

        public string DegreeDate { get; set; }

        public string University { get; set; }

        public string Specialization { get; set; }
        public List<string> InProgress { get; set; }
        public bool InProgressBool { get; set; }

    }
}
