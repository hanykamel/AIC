using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class ProfileVersion : Entity<Guid>
    {
        public ProfileVersion()
        {
            Documents = new HashSet<Document>();
            ProfileCertifications = new HashSet<ProfileCertification>();
            ProfileDegrees = new HashSet<ProfileDegree>();
            ProfileWorkExperience = new HashSet<ProfileWorkExperience>();
            ProfileInternships = new HashSet<ProfileInternship>();
            ProfileVacancies = new HashSet<ProfileVacancy>();
            ProfileTechnicalSkills = new HashSet<ProfileTechnicalSkill>();
        }

        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime AvaliableStartDate { get; set; }
        public string LinkToPortfolio { get; set; }
        public string JoinedUsAs { get; set; }
        public string JoinedIn { get; set; }
        public Guid ProfileId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual JoinUsProfile JoinUsProfile { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<ProfileCertification> ProfileCertifications { get; set; }
        public virtual ICollection<ProfileDegree> ProfileDegrees { get; set; }
        public virtual ICollection<ProfileWorkExperience> ProfileWorkExperience { get; set; }
        public virtual ICollection<ProfileInternship> ProfileInternships { get; set; }
        public virtual ICollection<ProfileVacancy> ProfileVacancies { get; set; }

        public virtual ICollection<ProfileTechnicalSkill> ProfileTechnicalSkills { get; set; }
    }
}
