using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class ProfileDegree : Entity<Guid>
    {
        public Guid ProfileVersionId { get; set; }
        public Guid DegreeId { get; set; }

        public virtual Degree Degree { get; set; }
        public virtual ProfileVersion ProfileVersion { get; set; }
    }
}
