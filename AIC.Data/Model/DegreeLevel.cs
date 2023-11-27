using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class DegreeLevel : Entity<int>
    {
        public DegreeLevel()
        {
            Degrees = new HashSet<Degree>();
        }

        //public string Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public virtual ICollection<Degree> Degrees { get; set; }
    }
}
