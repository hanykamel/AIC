using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class ProfileVacancy : Entity<Guid>
    {
        public int VacancyId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid ProfileVersionId { get; set; }
        public DateTime AppliedDate { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ProfileVersion ProfileVersion { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}
