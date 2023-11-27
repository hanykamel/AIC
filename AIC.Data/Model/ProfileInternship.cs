using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class ProfileInternship : Entity<Guid>
    {
        public Guid ProfileId { get; set; }
        public int InternshipId { get; set; }
        public Guid ProfileVersionId { get; set; }
        public DateTime AppliedDate { get; set; }

        public virtual Internship Internship { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ProfileVersion ProfileVersion { get; set; }
    }
}
