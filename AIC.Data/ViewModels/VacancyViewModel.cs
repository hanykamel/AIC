using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class VacancyViewModel
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string StartDate { get; set; }
        public string LinkToPortfolio { get; set; }
        public string ProfileId { get; set; }
        public string Email { get; set; }
        public string JoinedUsAs { get; set; }
        public string JoinedIn { get; set; }
        public List<string> JoinedUsAsLst { get; set; }
        public List<AcademicDegreeViewModel> AcademicDegrees { get; set; }
        public List<CertificatesViewModel> Certificates { get; set; }
        public List<WorkExperienceViewModel> WorkExperiences { get; set; }
        public List<TechnicalSkillsViewModel> TechnicalSkills { get; set; }
        public List<DocumentDBViewModel> Documents { get; set; }
        public CareerViewModelDB CareersViewModel { get; set; }
    }
}
