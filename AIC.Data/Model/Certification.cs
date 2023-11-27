using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class Certification : Entity<Guid>
    {
        public Certification()
        {
            ProfileCertifications = new HashSet<ProfileCertification>();
        }

        public string CertificateName { get; set; }
        public string CertifiedFrom { get; set; }
        public DateTime CertificateDate { get; set; }

        public virtual ICollection<ProfileCertification> ProfileCertifications { get; set; }
    }
}
