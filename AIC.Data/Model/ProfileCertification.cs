using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class ProfileCertification : Entity<Guid>
    {
        public Guid ProfileVersionId { get; set; }
        public Guid CertificationId { get; set; }

        public virtual Certification Certification { get; set; }
        public virtual ProfileVersion ProfileVersion { get; set; }
    }
}
