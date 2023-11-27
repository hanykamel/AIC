using AIC.Data.ViewModels;
using AIC.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.InternShips
{
    public class AddIntershipsCommand : IRequest<bool>
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
        [Required]
        public string InternshipId { get; set; }
        [Required]
        public string RefrenceNum { get; set; }
        public string JoinedUsAs { get; set; }
        public string JoinedIn { get; set; }
        public List<string> JoinedUsAsLst { get; set; }
        public List<AcademicDegreeViewModel> AcademicDegrees { get; set; }
        public List<CertificatesViewModel> Certificates { get; set; }
        public List<TechnicalSkillsViewModel> TechnicalSkills { get; set; }
        public IFormFileCollection UploadedCV { get; set; }
        public IFormFileCollection OtherDocuments { get; set; }
        public string AcademicDegreesStr { get; set; }
        public string CertificatesStr { get; set; }
        public string TechnicalSkillsStr { get; set; }
        public string DocumentsStr { get; set; }
    }
    public class AddInternshipsHandler : IRequestHandler<AddIntershipsCommand, bool>
    {
        private readonly IInternShipsBusiness _internshipBusiness;
        public AddInternshipsHandler(IInternShipsBusiness internshipBusiness)
        {
            _internshipBusiness = internshipBusiness;
        }

        public async Task<bool> Handle(AddIntershipsCommand request, CancellationToken cancellationToken)
        {
            return await _internshipBusiness.AddInternShips(request);
        }
    }
}
