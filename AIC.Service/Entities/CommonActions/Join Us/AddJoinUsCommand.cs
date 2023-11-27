using AIC.Data.ViewModels;
using AIC.Data.ViewModels.Lookups;
using AIC.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Join_Us
{
    public class AddJoinUsCommand : IRequest<bool>
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [Phone]
        //[RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$")]
        public string MobileNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public string StartDate { get; set; }
        public string LinkToPortfolio { get; set; }
        [Required]
        public string ProfileId { get; set; }
        public string JoinedUsAs { get; set; }
        public string JoinedIn { get; set; }
        public List<string> JoinedUsAsLst { get; set; }
        public List<AcademicDegreeViewModel> AcademicDegrees { get; set; }
        public List<CertificatesViewModel> Certificates { get; set; }
        public List<WorkExperienceViewModel> WorkExperiences { get; set; }
        public List<TechnicalSkillsViewModel> TechnicalSkills { get; set; }
        public IFormFileCollection UploadedCV { get; set; }    
        public IFormFileCollection OtherDocuments { get; set; }
        public string AcademicDegreesStr { get; set; }
        public string CertificatesStr { get; set; }
        public string WorkExperiencesStr { get; set; }
        public string TechnicalSkillsStr { get; set; }
        public string DocumentsStr { get; set; }
    }

    public class AddJoinUsHandler : IRequestHandler<AddJoinUsCommand, bool>
    {
        private readonly IJoinUsBusiness _joinUsBusiness;

        public AddJoinUsHandler(IJoinUsBusiness joinUsBusiness)
        {
            _joinUsBusiness = joinUsBusiness;
        }

        public async Task<bool> Handle(AddJoinUsCommand request, CancellationToken cancellationToken)
        {
            return await _joinUsBusiness.AddJoinUs(request);
        }
    }
}
