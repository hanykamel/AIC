using AutoMapper;
using AIC.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIC.Data.ViewModels.Lookups;
using Microsoft.AspNetCore.Identity;
using AIC.Service.Helper;
using AIC.Data.Model;
using AIC.Service.Entities.CommonActions.Join_Us;
using AIC.CrossCutting.MailService;
using AIC.SP.Middleware.SPViewModels;
using AIC.Service.Entities.CommonActions.ContactUs;
using AIC.Service.Entities.CommonActions.Vacancy;
using AIC.Service.Entities.CommonActions.InternShips;
using AIC.Data.Enums;

namespace AIC.Service.Mapper
{
    public class AutoMappingConfig : AutoMapper.Profile
    {
        public AutoMappingConfig()
        {
            // // Add as many of these lines as you need to map your objects

            CreateMap<CreateContactUsBaseViewModel, CreateContactUsFormCommand>().ReverseMap();

            #region Subscribers
            CreateMap<SubscriberViewModel, Subscriber>().ReverseMap();
            CreateMap<SubscriberVM, Subscriber>().ReverseMap();
            CreateMap<EmailAddress, Subscriber>()
                .ForMember(dst => dst.Email, src => src.MapFrom(e => e.Address))
                .ReverseMap();
            #endregion
            #region JoinUs
            CreateMap<ProfileVersion, AddJoinUsCommand>()
                .ForMember(p => p.StartDate, src => src.MapFrom(e => e.AvaliableStartDate.Date.ToString("yyyy-MM-dd")))
                .ForMember(p => p.BirthDate, src => src.MapFrom(e => e.BirthDate.Date.ToString("yyyy-MM-dd")))
                .ReverseMap();
            CreateMap<AddJoinUsCommand, ProfileVersion>()
                .ForMember(p => p.AvaliableStartDate, src => src.MapFrom(e => e.StartDate))
                .ReverseMap();
            CreateMap<ProfileVersion, JoinUsViewModel>()
                .ForMember(p => p.Email, src => src.MapFrom(e => e.Profile.Email))
                .ForMember(p => p.StartDate, src => src.MapFrom(e => e.AvaliableStartDate.Date.ToString("yyyy-MM-dd")))
                .ForMember(p => p.BirthDate, src => src.MapFrom(e => e.BirthDate.Date.ToString("yyyy-MM-dd"))).ReverseMap();
            CreateMap<JoinUsProfile, JoinUsApplicationViewModel>()
               .ForMember(p => p.Id, src => src.MapFrom(e => e.ProfileVersionId))
               .ForMember(p => p.Email, src => src.MapFrom(e => e.ProfileVersion.Profile.Email))
               .ForMember(p => p.AppliedDate, src => src.MapFrom(e => e.AppliedDate))
               .ForMember(p => p.FullName, src => src.MapFrom(e => e.ProfileVersion.FullName))
                .ForMember(p => p.CVURL, src => src.MapFrom(e => e.ProfileVersion.Documents.FirstOrDefault(d => d.DocumenyTypeId == (int)DocumentTypesEnum.CVs).DocumentUrl));

            #endregion
            #region Document
            CreateMap<Document, DocumentDBViewModel>()
                .ForMember(p => p.DocumenyType, src => src.MapFrom(e => e.DocumenyType))
                .ReverseMap();
            #endregion
            #region Lookups
            CreateMap<LookupsViewModel, DegreeLevel>().ReverseMap();
            CreateMap<LookupsViewModel, JobTypes>().ReverseMap();
            CreateMap<LookupsViewModel, JobTypes>().ReverseMap();
            CreateMap<DocumentTypeViewModel, DocumentType>().ReverseMap();
            #endregion
            #region Lookups

            #endregion
            #region Vacancies
            CreateMap<ProfileVersion, AddVacancyCommand>()
               .ForMember(p => p.StartDate, src => src.MapFrom(e => e.AvaliableStartDate.Date.ToString("yyyy-MM-dd")))
               .ForMember(p => p.BirthDate, src => src.MapFrom(e => e.BirthDate.Date.ToString("yyyy-MM-dd")))
               .ReverseMap();
            CreateMap<AddVacancyCommand, ProfileVersion>()
                .ForMember(p => p.AvaliableStartDate, src => src.MapFrom(e => e.StartDate))
                .ReverseMap();
            CreateMap<CareerViewModelDB, CareersViewModel>().ReverseMap();
            CreateMap<ProfileVersion, VacancyViewModel>()
               .ForMember(p => p.Email, src => src.MapFrom(e => e.Profile.Email))
               .ForMember(p => p.StartDate, src => src.MapFrom(e => e.AvaliableStartDate.Date.ToString("yyyy-MM-dd")))
               .ForMember(p => p.BirthDate, src => src.MapFrom(e => e.BirthDate.Date.ToString("yyyy-MM-dd")))
               .ReverseMap();
            CreateMap<ProfileVacancy, ListAppliedForCareersViewModel>()
               .ForMember(p => p.Id, src => src.MapFrom(e => e.ProfileVersionId))
               .ForMember(p => p.Email, src => src.MapFrom(e => e.Profile.Email))
               .ForMember(p => p.VacancyTitleAr, src => src.MapFrom(e => e.Vacancy.NameAr))
               .ForMember(p => p.VacancyTitleEn, src => src.MapFrom(e => e.Vacancy.NameEn))
               .ForMember(p => p.ReferenceNumber, src => src.MapFrom(e => e.Vacancy.ReferenceNumber))
               .ForMember(p => p.AppliedDate, src => src.MapFrom(e => e.AppliedDate))
               .ForMember(p => p.FullName, src => src.MapFrom(e => e.ProfileVersion.FullName))
               .ForMember(p => p.CVURL, src => src.MapFrom(e => e.ProfileVersion.Documents.FirstOrDefault(d => d.DocumenyTypeId == (int)DocumentTypesEnum.CVs).DocumentUrl));
            CreateMap<ProfileDegree, AcademicDegreeViewModel>()
               .ForMember(p => p.DegreeLevelId, src => src.MapFrom(e => e.Degree.DegreeLevelId))
               .ForMember(p => p.DegreeLevel, src => src.MapFrom(e => e.Degree.DegreeLevel))
               .ForMember(p => p.DegreeDate, src => src.MapFrom(e => e.Degree.DegreeDate.Date.ToString()))
               .ForMember(p => p.Specialization, src => src.MapFrom(e => e.Degree.Specialization))
               .ForMember(p => p.University, src => src.MapFrom(e => e.Degree.University))
               .ForMember(p => p.InProgressBool, src => src.MapFrom(e => e.Degree.InProgress));
            CreateMap<ProfileCertification, CertificatesViewModel>()
               .ForMember(p => p.CertificateName, src => src.MapFrom(e => e.Certification.CertificateName))
               .ForMember(p => p.CertifiedDate, src => src.MapFrom(e => e.Certification.CertificateDate.Date.ToString()))
               .ForMember(p => p.CertifiedFrom, src => src.MapFrom(e => e.Certification.CertifiedFrom));
            CreateMap<ProfileWorkExperience, WorkExperienceViewModel>()
               .ForMember(p => p.Company, src => src.MapFrom(e => e.workExperience.Company))
               .ForMember(p => p.CurrentJobBool, src => src.MapFrom(e => e.workExperience.CurrentJob))
               .ForMember(p => p.EndDate, src => src.MapFrom(e => e.workExperience.EndDate.Date.ToString()))
               .ForMember(p => p.StartDate, src => src.MapFrom(e => e.workExperience.StartDate.Date.ToString()))
               .ForMember(p => p.Job, src => src.MapFrom(e => e.workExperience.Job))
               .ForMember(p => p.JobTypeId, src => src.MapFrom(e => e.workExperience.JobTypeId))
               .ForMember(p => p.JobType, src => src.MapFrom(e => e.workExperience.JobTypes));
            CreateMap<ProfileTechnicalSkill, TechnicalSkillsViewModel>()
               .ForMember(p => p.SkillName, src => src.MapFrom(e => e.TechnicalSkills.TechnicalSkill))
               .ForMember(p => p.YearsOfExperience, src => src.MapFrom(e => e.TechnicalSkills.YearsOfExperience));
            CreateMap<Vacancy, VacancyItemViewModel>()
                .ForMember(p => p.Title, src => src.MapFrom(e => e.NameEn));
            #endregion
            #region Internships
            CreateMap<ProfileVersion, AddIntershipsCommand>()
               .ForMember(p => p.StartDate, src => src.MapFrom(e => e.AvaliableStartDate.Date.ToString("yyyy-MM-dd")))
               .ForMember(p => p.BirthDate, src => src.MapFrom(e => e.BirthDate.Date.ToString("yyyy-MM-dd")))
               .ReverseMap();
            CreateMap<AddIntershipsCommand, ProfileVersion>()
             .ForMember(p => p.AvaliableStartDate, src => src.MapFrom(e => e.StartDate))
             .ReverseMap();
            CreateMap<InternshipViewModelDB, InternshipsViewModel>().ReverseMap();
            CreateMap<ProfileVersion, InternshipProfileViewModel>()
            .ForMember(p => p.Email, src => src.MapFrom(e => e.Profile.Email))
            .ForMember(p => p.StartDate, src => src.MapFrom(e => e.AvaliableStartDate.Date.ToString("yyyy-MM-dd")))
            .ForMember(p => p.BirthDate, src => src.MapFrom(e => e.BirthDate.Date.ToString("yyyy-MM-dd"))).ReverseMap();

            CreateMap<ProfileInternship, AppliedForInternshipsViewModel>()
             .ForMember(p => p.Id, src => src.MapFrom(e => e.ProfileVersionId))
             .ForMember(p => p.Email, src => src.MapFrom(e => e.Profile.Email))
             .ForMember(p => p.InternshipTitleEn, src => src.MapFrom(e => e.Internship.NameEn))
             .ForMember(p => p.InternshipTitleAr, src => src.MapFrom(e => e.Internship.NameAr))
             .ForMember(p => p.ReferenceNumber, src => src.MapFrom(e => e.Internship.ReferenceNumber))
             .ForMember(p => p.AppliedDate, src => src.MapFrom(e => e.AppliedDate))
             .ForMember(p => p.FullName, src => src.MapFrom(e => e.ProfileVersion.FullName))
             .ForMember(p => p.CVURL, src => src.MapFrom(e => e.ProfileVersion.Documents.FirstOrDefault(d => d.DocumenyTypeId == (int)DocumentTypesEnum.CVs).DocumentUrl));

            CreateMap<Internship, InternshipItemViewModel>()
                .ForMember(p => p.Title, src => src.MapFrom(e => e.NameEn));
            #endregion

        }
    }
}

