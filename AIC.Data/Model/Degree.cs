using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class Degree : Entity<Guid>
    {
        public Degree()
        {
            ProfileDegrees = new HashSet<ProfileDegree>();
        }

        //public string Id { get; set; }
        public int DegreeLevelId { get; set; }
        public DateTime DegreeDate { get; set; }
        public string University { get; set; }
        public string Specialization { get; set; }
        public bool InProgress { get; set; }
        public virtual DegreeLevel DegreeLevel { get; set; }
        public virtual ICollection<ProfileDegree> ProfileDegrees { get; set; }
    }
}
