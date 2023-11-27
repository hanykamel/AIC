using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.Model
{
    public partial class TechnicalSkills : Entity<Guid>
    {
        public TechnicalSkills()
        {
            ProfileTechnicalSkills = new HashSet<ProfileTechnicalSkill>();
        }
        //public string Id { get; set; }
        public string TechnicalSkill { get; set; }
        public int YearsOfExperience { get; set; }

        public virtual ICollection<ProfileTechnicalSkill> ProfileTechnicalSkills { get; set; }
    }
}
