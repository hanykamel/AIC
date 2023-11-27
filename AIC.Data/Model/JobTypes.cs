using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.Model
{
    public partial class JobTypes : Entity<int>
    {
        public JobTypes()
        {
            WorkExperiences = new HashSet<WorkExperience>();
        }
        //public string Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }

        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
    }
}
