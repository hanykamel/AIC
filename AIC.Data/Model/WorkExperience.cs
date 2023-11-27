using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class WorkExperience : Entity<Guid>
    {
        public WorkExperience()
        {
            ProfileWorkExperience = new HashSet<ProfileWorkExperience>();
        }

        //public string Id { get; set; }
        public string Job { get; set; }
        public int? JobTypeId { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? CurrentJob { get; set; }

        public virtual JobTypes JobTypes { get; set; }
        public virtual ICollection<ProfileWorkExperience> ProfileWorkExperience { get; set; }
    }
}
