using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.Model
{
    public partial class ProfileTechnicalSkill : Entity<Guid>
    {
        public Guid ProfileVersionId { get; set; }
        public Guid TechnicalSkillId { get; set; }

        public virtual TechnicalSkills TechnicalSkills { get; set; }
        public virtual ProfileVersion ProfileVersion { get; set; }
    }
}
