using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class Internship : Entity<int>
    {
        public Internship()
        {
            ProfileInternships = new HashSet<ProfileInternship>();
        }

        //public int SharepointId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ReferenceNumber { get; set; }

        public virtual ICollection<ProfileInternship> ProfileInternships { get; set; }
    }
}
