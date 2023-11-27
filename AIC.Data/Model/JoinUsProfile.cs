using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class JoinUsProfile : Entity<Guid>
    {
        public Guid ProfileVersionId { get; set; }
        public DateTime AppliedDate { get; set; }

        public virtual ProfileVersion ProfileVersion { get; set; }
    }
}
