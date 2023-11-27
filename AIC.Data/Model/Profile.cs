using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class Profile : Entity<Guid>
    {
        public Profile()
        {
            ProfileInternships = new HashSet<ProfileInternship>();
            ProfileVacancies = new HashSet<ProfileVacancy>();
            ProfileVersions = new HashSet<ProfileVersion>();
        }

        //public string Id { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ProfileInternship> ProfileInternships { get; set; }
        public virtual ICollection<ProfileVacancy> ProfileVacancies { get; set; }
        public virtual ICollection<ProfileVersion> ProfileVersions { get; set; }
    }
}
