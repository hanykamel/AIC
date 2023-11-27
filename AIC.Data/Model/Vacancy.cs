using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class Vacancy : Entity<int>
    {
        public Vacancy()
        {
            ProfileVacancies = new HashSet<ProfileVacancy>();
        }

        //public int SharepointId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ReferenceNumber { get; set; }

        public virtual ICollection<ProfileVacancy> ProfileVacancies { get; set; }
    }
}
