using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class ProfileWorkExperience : Entity<Guid>
    {
        public Guid ProfileVersionId { get; set; }
        public Guid workExperienceId { get; set; }

        public virtual WorkExperience workExperience { get; set; }
        public virtual ProfileVersion ProfileVersion { get; set; }
    }
}
