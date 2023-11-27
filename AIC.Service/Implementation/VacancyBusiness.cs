using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Interfaces;
using AIC.CrossCutting.MailService;
using AIC.CrossCutting.Models.MvcModels;
using AIC.Data.Enums;
using AIC.Data.Model;
using AIC.Data.ViewModels;
using AIC.Repository;
using AIC.Service.Entities.CommonActions.Vacancy;
using AIC.Service.Interfaces;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.SPViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Service.Implementation
{
    public class VacancyBusiness : IVacancyBusiness
    {
        #region Properties
        readonly private IRepository<ProfileVersion, Guid> _profileVersionRepository;
        readonly private IRepository<Degree, Guid> _degreeRepository;
        readonly private IRepository<ProfileDegree, Guid> _profileDegreeRepository;
        readonly private IRepository<Certification, Guid> _certificationRepository;
        readonly private IRepository<ProfileCertification, Guid> _profileCertification;
        readonly private IRepository<WorkExperience, Guid> _workExperienceRepository;
        readonly private IRepository<ProfileWorkExperience, Guid> _profileWorkExperienceRepository;
        readonly private IRepository<TechnicalSkills, Guid> _technicalSkillsRepository;
        readonly private IRepository<ProfileTechnicalSkill, Guid> _profileTechnicalSkillsRepository;
        readonly private IRepository<Data.Model.Profile, Guid> _profileRepository;
        readonly private IRepository<Document, Guid> _documentRepository;
        readonly private IRepository<DocumentType, int> _documentTypeRepository;
        readonly private IRepository<Vacancy, int> _vacancyRepository;
        private readonly IMapper _mapper;
        private ISendEmail _sendEmailService;
        private readonly IConfiguration _config;
        private readonly ICustomCryptography _crypt;
        readonly private IService<DocumentsViewModel> _documentsService;
        readonly private IUnitOfWork _unitOfWork;
        private readonly IService<CareersViewModel> _careersService;
        readonly private IRepository<ProfileVacancy, Guid> _profileVacancyRepository;
        private string _lang { get; set; }
        #endregion
        #region Ctor
        public VacancyBusiness(IRepository<ProfileVersion, Guid> profileVersionRepository,
                              IMapper mapper,
                              IRepository<Degree, Guid> degreeRepository,
                              IRepository<ProfileDegree, Guid> profileDegreeRepository,
                              IRepository<Certification, Guid> certificationRepository,
                              IRepository<ProfileCertification, Guid> profileCertification,
                              IRepository<WorkExperience, Guid> workExperienceRepository,
                              IRepository<ProfileWorkExperience, Guid> profileWorkExperienceRepository,
                              IRepository<TechnicalSkills, Guid> technicalSkillsRepository,
                              IRepository<ProfileTechnicalSkill, Guid> profileTechnicalSkillsRepository,
                              IRepository<Data.Model.Profile, Guid> profileRepository,
                              ISendEmail sendEmailService,
                              IConfiguration config,
                              ICustomCryptography crypt,
                              IService<DocumentsViewModel> documentsService,
                              IRepository<Document, Guid> documentRepository,
                              IRepository<Vacancy, int> vacancyRepository,
                              IRepository<DocumentType, int> documentTypeRepository,
                              IUnitOfWork unitOfWork,
                              IService<CareersViewModel> careersService,
                              IRepository<ProfileVacancy, Guid> profileVacancyRepository)
        {
            _unitOfWork = unitOfWork;
            _profileVersionRepository = profileVersionRepository;
            _mapper = mapper;
            _degreeRepository = degreeRepository;
            _certificationRepository = certificationRepository;
            _profileCertification = profileCertification;
            _workExperienceRepository = workExperienceRepository;
            _profileWorkExperienceRepository = profileWorkExperienceRepository;
            _technicalSkillsRepository = technicalSkillsRepository;
            _profileTechnicalSkillsRepository = profileTechnicalSkillsRepository;
            _profileDegreeRepository = profileDegreeRepository;
            _profileRepository = profileRepository;
            _sendEmailService = sendEmailService;
            _config = config;
            _crypt = crypt;
            _documentsService = documentsService;
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;
            _careersService = careersService;
            _profileVacancyRepository = profileVacancyRepository;
            _vacancyRepository = vacancyRepository;
        }
        #endregion
        #region AIC
        public async Task<bool> AddUserProfile(AddUserProfileVacancyCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("NullModel");
            
            _lang = command.lang;
            if (!CheckVacancyExpiration(command.VacancyId))
                throw new ModelExpiredException("VacancyExpired");

            var vacancy = getCareersById(command.VacancyId);
            string submittionFormUrl = string.Empty;
            Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
            List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = command.Email, Name = command.Email } };
            var userProfile = _profileRepository.GetAll(p => p.Email == command.Email).FirstOrDefault();
            if (userProfile is null)
            {
                _profileRepository.Add(new Data.Model.Profile() { Email = command.Email });
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                if (!CheckIfUSerApplyVacationBefore(userProfile.Id, command.VacancyId))
                    throw new AppliedBeforeException("VacancyAppliedBefore");
            }
            //Send Submittion Form URL
            submittionFormUrl = _config.GetValue<string>("AngularComponents:VacancyUrl").Replace("{email}", _crypt.Encrypt(command.Email));
            var expirationDate = SetExpirationDate(DateTime.Now);
            submittionFormUrl = submittionFormUrl.Replace("{date}", (_crypt.Encrypt(expirationDate.ToString())));
            submittionFormUrl = submittionFormUrl.Replace("{vacancyId}", (_crypt.Encrypt(command.VacancyId.ToString())));
            tempPlaceholder.Add("#FormURLEN", submittionFormUrl + "en");
            tempPlaceholder.Add("#FormURLAR", submittionFormUrl + "ar");
            tempPlaceholder.Add("#NameEn#", vacancy.Title);
            tempPlaceholder.Add("#NameAr#", vacancy.TitleAr);
            tempPlaceholder.Add("#InternRefNumEn#", vacancy.ReferenceNumber);
            tempPlaceholder.Add("#InternRefNumAr#", vacancy.ReferenceNumber);
            SendMail(toAddresses, "Careers And Internships", MailTypesEnum.AICCareersAndInternships, tempPlaceholder);
            return true;
        }
        public async Task<bool> AddVacancy(AddVacancyCommand command)
        {

            Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
            List<EmailAddress> toAddresses = new List<EmailAddress>();
            if (command == null)
                throw new ArgumentNullException("NullModel");

            var vacancyId = _crypt.Decrypt(command.VacancyId);
            var vacancy = getCareersById(int.Parse(vacancyId));

            var profileVersionEntity = _mapper.Map<ProfileVersion>(command);
            AddProfileVersions(profileVersionEntity);
            AddProfileVacancies(profileVersionEntity, int.Parse(vacancyId));
            AddAcademicDegree(command.AcademicDegreesStr, profileVersionEntity);
            AddCertificates(command.CertificatesStr, profileVersionEntity);
            AddWorkExperience(command.WorkExperiencesStr, profileVersionEntity);
            AddTechnicalSkills(command.TechnicalSkillsStr, profileVersionEntity);

            // Upload Files
            await UploadDocuments(command.UploadedCV, ListsNames.UploadCvs, profileVersionEntity);
            await UploadDocuments(command.OtherDocuments, ListsNames.OtherDocument, profileVersionEntity);

            await _unitOfWork.SaveChangesAsync();

            if (command.UploadedCV == null && command.OtherDocuments == null)
            {
                AppendOldDocuments(command.DocumentsStr, profileVersionEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            // Send Mail
            if (profileVersionEntity is not null)
            {
                var profile = _profileRepository.GetAll(p => p.Id == profileVersionEntity.ProfileId).FirstOrDefault();
                toAddresses.Add(new EmailAddress { Address = profile.Email, Name = profile.Email });
            }
            tempPlaceholder.Add("#FormTitleEn#", FormTypes.CareersFormEn);
            tempPlaceholder.Add("#FormTitleAr#", FormTypes.CareersFormAr);
            tempPlaceholder.Add("#NameEn#", vacancy.Title);
            tempPlaceholder.Add("#NameAr#", vacancy.TitleAr);
            tempPlaceholder.Add("#InternRefNumEn#", vacancy.ReferenceNumber);
            tempPlaceholder.Add("#InternRefNumAr#", vacancy.ReferenceNumber);
            SendMail(toAddresses, "Careers And Internships", MailTypesEnum.SubmissionFormConfirmed, tempPlaceholder);


            return true;
        }
        public ProfileVersion AddProfileVersions(ProfileVersion profileVersionsEntity)
        {
            if (profileVersionsEntity == null)
                throw new ArgumentNullException("NullModel");
            _profileVersionRepository.Add(profileVersionsEntity);
            return profileVersionsEntity;
        }
        public bool AddProfileVacancies(ProfileVersion profileVersionsEntity, int vacancyId)
        {
            if (profileVersionsEntity == null || vacancyId <= 0)
                throw new ArgumentNullException("NullModel");

            if (!CheckIfUSerApplyVacationBefore(profileVersionsEntity.ProfileId, vacancyId))
                throw new AppliedBeforeException("VacancyAppliedBefore");


            ProfileVacancy profileVacancy = new ProfileVacancy()
            {
                VacancyId = vacancyId,
                ProfileId = profileVersionsEntity.ProfileId,
                ProfileVersionId = profileVersionsEntity.Id,
                AppliedDate = DateTime.Now
            };
            _profileVacancyRepository.Add(profileVacancy);
            return true;
        }
        public bool AddAcademicDegree(string academicDegreesStr, ProfileVersion profileVersionsEntity)
        {
            if (academicDegreesStr == null)
                return false;
            var academicDegrees = JsonConvert.DeserializeObject<List<AcademicDegreeViewModel>>(academicDegreesStr);

            foreach (var degree in academicDegrees)
            {
                if (degree.InProgress is not null && degree.InProgress.Count > 0)
                {
                    degree.InProgressBool = true;
                }
                Degree degreeModel = new Degree()
                {
                    DegreeLevelId = degree.DegreeLevelId,
                    DegreeDate = degree.DegreeDate == " " ? DateTime.MinValue : Convert.ToDateTime(degree.DegreeDate),
                    University = degree.University,
                    Specialization = degree.Specialization,
                    InProgress = degree.InProgressBool
                };
                _degreeRepository.Add(degreeModel);
                ProfileDegree profileDegree = new ProfileDegree()
                {
                    Degree = degreeModel,
                    ProfileVersion = profileVersionsEntity
                };
                _profileDegreeRepository.Add(profileDegree);
            }

            return true;


        }
        public bool AddCertificates(string certificatesStr, ProfileVersion profileVersionsEntity)
        {

            if (certificatesStr == null)
                return false;
            var certificates = JsonConvert.DeserializeObject<List<CertificatesViewModel>>(certificatesStr);

            foreach (var certificate in certificates)
            {
                Certification certificateModel = new Certification()
                {
                    CertificateName = certificate.CertificateName,
                    CertificateDate = certificate.CertifiedDate == " " ? DateTime.MinValue : Convert.ToDateTime(certificate.CertifiedDate),
                    CertifiedFrom = certificate.CertifiedFrom,
                };
                _certificationRepository.Add(certificateModel);
                ProfileCertification profileDegree = new ProfileCertification()
                {
                    Certification = certificateModel,
                    ProfileVersion = profileVersionsEntity
                };
                _profileCertification.Add(profileDegree);
            }
            return true;

        }
        public bool AddWorkExperience(string workExperiencesStr, ProfileVersion profileVersionsEntity)
        {

            if (workExperiencesStr == null)
                return false;
            var workExperiences = JsonConvert.DeserializeObject<List<WorkExperienceViewModel>>(workExperiencesStr);

            foreach (var workExperience in workExperiences)
            {
                if (workExperience.CurrentJob is not null && workExperience.CurrentJob.Count > 0)
                {
                    workExperience.CurrentJobBool = true;
                }
                WorkExperience model = new WorkExperience()
                {
                    Company = workExperience.Company == null ? "null" : workExperience.Company,
                    CurrentJob = workExperience.CurrentJobBool,
                    StartDate = workExperience.StartDate == " " ? DateTime.MinValue : Convert.ToDateTime(workExperience.StartDate),
                    EndDate = workExperience.EndDate == " " ? DateTime.MinValue : Convert.ToDateTime(workExperience.EndDate),
                    Job = workExperience.Job == null ? "null" : workExperience.Job,
                    JobTypeId = workExperience.JobTypeId,
                };
                _workExperienceRepository.Add(model);
                ProfileWorkExperience profileWorkExperience = new ProfileWorkExperience()
                {
                    workExperience = model,
                    ProfileVersion = profileVersionsEntity,
                };
                _profileWorkExperienceRepository.Add(profileWorkExperience);
            }
            return true;

        }
        public bool AddTechnicalSkills(string technicalSkillsStr, ProfileVersion profileVersionsEntity)
        {

            if (technicalSkillsStr == null)
                return false;

            var technicalSkills = JsonConvert.DeserializeObject<List<TechnicalSkillsViewModel>>(technicalSkillsStr);

            foreach (var skill in technicalSkills)
            {
                TechnicalSkills model = new TechnicalSkills()
                {
                    TechnicalSkill = skill.SkillName == null ? "null" : skill.SkillName,
                    YearsOfExperience = skill.YearsOfExperience,
                };
                _technicalSkillsRepository.Add(model);
                ProfileTechnicalSkill profileTechnicalSkill = new ProfileTechnicalSkill()
                {
                    TechnicalSkills = model,
                    ProfileVersion = profileVersionsEntity
                };
                _profileTechnicalSkillsRepository.Add(profileTechnicalSkill);
            }
            return true;
        }
        public async Task<VacancyViewModel> GetProfileVacancy(GetVacancyProfileCommand command)
        {
            if (command.Email is null || command.Date is null || command.VacancyId == "null")
                throw new ArgumentNullException("NullModel");
            _lang = command.Lang;
            CareerViewModelDB vacancyVM = new CareerViewModelDB();
            var decryptedEmail = _crypt.Decrypt(command.Email);
            var decryptedDate = _crypt.Decrypt(command.Date);
            var decryptedVacancyId = _crypt.Decrypt(command.VacancyId);

            var profileEntity = _profileRepository.GetAll(p => p.Email == decryptedEmail).Include(p => p.ProfileVacancies).FirstOrDefault();
            if (profileEntity is null)
                throw new ArgumentNullException("NullModel");

            if (!CheckVacancyExpiration(Int32.Parse(decryptedVacancyId))) // Check Vacany Expiration
                throw new ModelExpiredException("VacancyExpired");

            if (!ValidateFormUrl(Convert.ToDateTime(decryptedDate)))     // Check Form Url Expiration
                throw new FormURLNotValidException("FormValidationError");

            if (!CheckIfUSerApplyVacationBefore(profileEntity.Id, int.Parse(decryptedVacancyId))) // check if user applied before
                throw new AppliedBeforeException("VacancyAppliedBefore");

            var vacancy = getCareersById(Int32.Parse(decryptedVacancyId));
            if (vacancy is not null)
            {
                vacancyVM = _mapper.Map<CareerViewModelDB>(vacancy);
            }
            else
            {
                throw new ArgumentNullException("NullModel");
            }

            //var profileVacancies = _profileVacancyRepository.GetAll(p => p.VacancyId == Int32.Parse(decryptedVacancyId) && p.Profile.Email == decryptedEmail)
            //                     .Include(p => p.ProfileVersion).Include(p => p.Profile).FirstOrDefault();

            //if (profileVacancies is not null)  // has already profile vacancy
            //{
            var profileVersionEntity = _profileVersionRepository.GetAll(i => i.ProfileId == profileEntity.Id).Include(i => i.Profile)
                    .OrderByDescending(i => i.Created).FirstOrDefault();
            if (profileVersionEntity is not null)
            {
                var vacancyViewModel = _mapper.Map<VacancyViewModel>(profileVersionEntity);

                vacancyViewModel.AcademicDegrees =
                 _profileDegreeRepository.GetAll(i => i.ProfileVersionId == profileVersionEntity.Id).Include(i => i.Degree).AsEnumerable()
                    .Select(item => new AcademicDegreeViewModel()
                    {
                        DegreeLevelId = item.Degree.DegreeLevelId,
                        DegreeDate = item.Degree.DegreeDate == DateTime.MinValue ? " " : item.Degree.DegreeDate.Date.ToString("yyyy-MM-dd"),
                        Specialization = item.Degree.Specialization,
                        University = item.Degree.University,
                        InProgressBool = item.Degree.InProgress
                    }).ToList();
                vacancyViewModel.Certificates =
                    _profileCertification.GetAll(p => p.ProfileVersionId == profileVersionEntity.Id).Include(p => p.Certification).AsEnumerable()
                    .Select(item => new CertificatesViewModel()
                    {
                        CertificateName = item.Certification.CertificateName,
                        CertifiedDate = item.Certification.CertificateDate == DateTime.MinValue ? " " : item.Certification.CertificateDate.Date.ToString("yyyy-MM-dd"),
                        CertifiedFrom = item.Certification.CertifiedFrom
                    }).ToList();
                vacancyViewModel.WorkExperiences =
                    _profileWorkExperienceRepository.GetAll(w => w.ProfileVersionId == profileVersionEntity.Id).Include(w => w.workExperience).AsEnumerable()
                    .Select(item => new WorkExperienceViewModel()
                    {
                        Company = item.workExperience.Company == "null" ? " " : item.workExperience.Company,
                        CurrentJobBool = item.workExperience.CurrentJob,
                        EndDate = item.workExperience.EndDate == DateTime.MinValue ? " " : item.workExperience.EndDate.Date.ToString("yyyy-MM-dd"),
                        StartDate = item.workExperience.StartDate == DateTime.MinValue ? " " : item.workExperience.StartDate.Date.ToString("yyyy-MM-dd"),
                        Job = item.workExperience.Job == "null" ? " " : item.workExperience.Job,
                        JobTypeId = item.workExperience.JobTypeId
                    }).ToList();
                vacancyViewModel.TechnicalSkills =
                    _profileTechnicalSkillsRepository.GetAll(t => t.ProfileVersionId == profileVersionEntity.Id).Include(t => t.TechnicalSkills).AsEnumerable()
                    .Select(item => new TechnicalSkillsViewModel()
                    {
                        SkillName = item.TechnicalSkills.TechnicalSkill == "null" ? " " : item.TechnicalSkills.TechnicalSkill,
                        YearsOfExperience = item.TechnicalSkills.YearsOfExperience
                    }).ToList();

                vacancyViewModel.Documents =
                    _documentRepository.GetAll(d => d.ProfileVersionId == profileVersionEntity.Id).Include(t => t.DocumenyType).AsEnumerable()
                    .Select(item => new DocumentDBViewModel()
                    {
                        ContentType = item.ContentType,
                        DisplayTitle = item.DisplayTitle,
                        DocumentUrl = item.DocumentUrl,
                        DocumenyTypeId = item.DocumenyType.Id,
                        Extention = item.Extention,
                        FolderUrl = item.FolderUrl,
                        ProfileVersionId = item.ProfileVersionId,
                        SharepointId = item.SharepointId,
                        Size = item.Size
                    }).ToList();
                vacancyViewModel.CareersViewModel = vacancyVM;
                var mappedVacancyViewModel = MapCheckboxToUI(vacancyViewModel);
                return mappedVacancyViewModel;
            }
            //else
            //{
            //    throw new ArgumentNullException("NullModel");
            //}

            // first time apply vacancy
            return new VacancyViewModel
            {
                Email = decryptedEmail,
                ProfileId = profileEntity.Id.ToString(),
                CareersViewModel = vacancyVM
            };

        }

        public ListAppliedForCareersListViewModel ListAppliedForCareers(ListAppliedForCareersCommand query)
        {
            ListAppliedForCareersListViewModel result = new ListAppliedForCareersListViewModel();
            ListFilter emailFilter = null;
            ListFilter appliedDateFromFilter = null;
            ListFilter appliedDateToFilter = null;
            ListFilter vacancyIdFilter = null;
            ListFilter universityFilter = null;
            ListFilter ageFilter = null;
            ListFilter refNum = null;
            if (query.Filters is not null && query.Filters.Count() > 0)
            {
                emailFilter = query.Filters.Where(t => t.Field == "Email").FirstOrDefault();
                appliedDateFromFilter = query.Filters.Where(t => t.Field == "AppliedDateFrom").FirstOrDefault();
                appliedDateToFilter = query.Filters.Where(t => t.Field == "AppliedDateTo").FirstOrDefault();
                vacancyIdFilter = query.Filters.Where(t => t.Field == "VacancyId").FirstOrDefault();
                universityFilter = query.Filters.Where(t => t.Field == "University").FirstOrDefault();
                ageFilter = query.Filters.Where(t => t.Field == "Age").FirstOrDefault();
                refNum = query.Filters.Where(t => t.Field == "RefNum").FirstOrDefault();
            }
            var res = _profileVacancyRepository.GetAll()
                .Include(r => r.ProfileVersion).ThenInclude(v => v.ProfileDegrees).ThenInclude(p => p.Degree)
                .Include(r => r.ProfileVersion).ThenInclude(v => v.Documents)
                .Include(r => r.Vacancy)
                .Include(r => r.Profile).AsSplitQuery()
                .Where(
                t => ((emailFilter != null && !string.IsNullOrEmpty(emailFilter.Value))? t.Profile.Email.ToLower().Contains(emailFilter.Value.ToLower()) : true) &&
                ((appliedDateFromFilter != null && !string.IsNullOrEmpty(appliedDateFromFilter.Value)) ? DateTime.Parse(appliedDateFromFilter.Value) <= t.AppliedDate  : true) &&
                ((appliedDateToFilter != null && !string.IsNullOrEmpty(appliedDateToFilter.Value)) ? DateTime.Parse(appliedDateToFilter.Value) >= t.AppliedDate : true) &&
                ((vacancyIdFilter != null && !string.IsNullOrEmpty(vacancyIdFilter.Value) && vacancyIdFilter.Value != "-1") ? t.VacancyId.ToString() ==  vacancyIdFilter.Value: true) &&
                ((refNum != null && !string.IsNullOrEmpty(refNum.Value)) ? t.Vacancy.ReferenceNumber == refNum.Value: true) &&
                ((universityFilter != null && !string.IsNullOrEmpty(universityFilter.Value) )? t.ProfileVersion.ProfileDegrees.Any(d => d.Degree.University.Contains(universityFilter.Value)) : true)&&
                ((ageFilter != null && !string.IsNullOrEmpty(ageFilter.Value) && t.ProfileVersion.BirthDate.Date <= DateTime.Now.Date.AddYears(-int.Parse(ageFilter.Value))) ? (DateTime.Now.Date.Year - t.ProfileVersion.BirthDate.Year) == int.Parse(ageFilter.Value) : true) &&
                ((ageFilter != null && !string.IsNullOrEmpty(ageFilter.Value) && t.ProfileVersion.BirthDate.Date > DateTime.Now.Date.AddYears(-int.Parse(ageFilter.Value))) ? (DateTime.Now.Date.Year - t.ProfileVersion.BirthDate.Year -1) == int.Parse(ageFilter.Value) : true)
                ).AsNoTrackingWithIdentityResolution().ToList();
            var pagedResult = res.Skip(query.PageSize * query.PageIndex).Take(query.PageSize).ToList();
            result.TotalCount = res.Count();
            result.List = _mapper.Map<List<ListAppliedForCareersViewModel>>(pagedResult);
            return result;
        }
        public VacancyViewModel GetProfileVersionById(GetProfileVersionByIdCommand getProfileVersionByIdCommand)
        {
            var profileVersion = _profileVersionRepository.GetAll(p => p.Id == getProfileVersionByIdCommand.Id)
                .Include(p => p.Profile)
                .Include(p => p.ProfileVacancies)
                .Include(p => p.ProfileDegrees).ThenInclude(d => d.Degree).ThenInclude(d => d.DegreeLevel)
                .Include(p => p.ProfileCertifications).ThenInclude(d => d.Certification)
                .Include(p => p.ProfileWorkExperience).ThenInclude(d => d.workExperience).ThenInclude(w => w.JobTypes)
                .Include(p => p.ProfileTechnicalSkills).ThenInclude(d => d.TechnicalSkills)
                .Include(p => p.Documents).ThenInclude(d => d.DocumenyType).AsSplitQuery().AsNoTrackingWithIdentityResolution()
                .FirstOrDefault();
            if (profileVersion is null)
                throw new NotFoundException("ProfileVersionNotFound");
            var result = _mapper.Map<VacancyViewModel>(profileVersion);
            result.AcademicDegrees = _mapper.Map<List<AcademicDegreeViewModel>>(profileVersion.ProfileDegrees);
            result.Certificates = _mapper.Map<List<CertificatesViewModel>>(profileVersion.ProfileCertifications);
            result.WorkExperiences = _mapper.Map<List<WorkExperienceViewModel>>(profileVersion.ProfileWorkExperience);
            result.TechnicalSkills = _mapper.Map<List<TechnicalSkillsViewModel>>(profileVersion.ProfileTechnicalSkills);
            result.Documents = _mapper.Map<List<DocumentDBViewModel>>(profileVersion.Documents);
            var vacancyId = profileVersion.ProfileVacancies.FirstOrDefault()?.VacancyId;
            if (vacancyId is not null)
            {

                CareersViewModel vacancy;
                try
                {
                    vacancy = _careersService.GetById("", vacancyId.Value);

                }
                catch (NotFoundException ex)
                {
                    vacancy = null;
                }
                if (vacancy is not null)
                    result.CareersViewModel = _mapper.Map<CareerViewModelDB>(vacancy);
            }
            return result;
        }

        public bool DeleteById(DeleteVacancyProfileByIdCommand deleteVacancyProfileByIdCommand)
        {
            var profileVersion = _profileVersionRepository.GetAll(p => p.Id == deleteVacancyProfileByIdCommand.Id)
                .Include(p => p.ProfileVacancies)
                .FirstOrDefault();
            if (profileVersion is null)
                throw new NotFoundException("ProfileVersionNotFound");
            profileVersion.IsDeleted = true;
            profileVersion.ProfileVacancies.FirstOrDefault().IsDeleted = true;
            _profileVersionRepository.Update(profileVersion);
            _unitOfWork.SaveChanges();
            return true;
        }
        public List<VacancyItemViewModel> GetAll(GetAllVacanciesCommand request)
        {
            var items = _vacancyRepository.GetAll();
            var result = _mapper.Map<List<VacancyItemViewModel>>(items);
            return result;
        }
        #endregion
        #region Helper

        public DateTime SetExpirationDate(DateTime date)
        {
            return date.AddHours(12);
        }
        public void SendMail(List<EmailAddress> toAddresses, string mailSubject, MailTypesEnum mailType, Dictionary<string, string> data, string errorMsg = null)
        {
            _sendEmailService.SendMail(toAddresses, mailSubject, mailType, data);
        }
        public bool CheckVacancyExpiration(int vacancyId)
        {
            if (vacancyId > 0)
            {
                var vacancy = getCareersById(vacancyId);
                if (vacancy is not null && vacancy.VacancyExpiryDate != DateTime.MinValue)
                    return ValidateFormUrl(vacancy.VacancyExpiryDate);
                else
                    return false;
            }
            return false;
        }
        public CareersViewModel getCareersById(int vacancyId)
        {
            if (vacancyId <= 0)
                return null;
            try
            {
                var vacancy = _careersService.GetById("", vacancyId);
                return vacancy;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public bool ValidateFormUrl(DateTime expirationDate)
        {
            if (expirationDate != DateTime.MinValue)
            {
                if (expirationDate <= DateTime.Now)
                    return false;                  //Expired
                else
                    return true;                 //valid date
            }
            else
                return false;

        }
        public bool CheckIfUSerApplyVacationBefore(Guid profileId, int vacancyId)
        {

            var profileVacancy = _profileVacancyRepository.GetAll(i => i.ProfileId == profileId
                                                          && i.VacancyId == vacancyId ).FirstOrDefault();
            if (profileVacancy is null)
                return true;    // not applied before
            else
                return false;   // applied before

        }
        public VacancyViewModel MapCheckboxToUI(VacancyViewModel vacancyViewModel)
        {
            if (vacancyViewModel is null)
                return null;
            if (vacancyViewModel.WorkExperiences is not null)
            {

                foreach (var workExperience in vacancyViewModel.WorkExperiences)
                {
                    if (workExperience.CurrentJobBool == true)
                    {
                        string str = " ";
                        List<string> strLst = new List<string>();
                        strLst.Add(str);
                        workExperience.CurrentJob = strLst;
                    }
                }
            }
            if (vacancyViewModel.AcademicDegrees is not null)
            {

                foreach (var academicDegree in vacancyViewModel.AcademicDegrees)
                {
                    if (academicDegree.InProgressBool == true)
                    {
                        string str = " ";
                        List<string> strLst = new List<string>();
                        strLst.Add(str);
                        academicDegree.InProgress = strLst;
                    }
                }
            }
            if (vacancyViewModel.JoinedUsAs != null)
            {
                List<string> joinedUsLst = new List<string>();
                joinedUsLst.Add(vacancyViewModel.JoinedUsAs);
                vacancyViewModel.JoinedUsAsLst = joinedUsLst;
            }
            return vacancyViewModel;
        }
        public async Task UploadDocuments(IFormFileCollection files, string listName, ProfileVersion profileVersion)
        {
            if (files != null && files.Count > 0)
            {
                string documentUrl = string.Empty;
                foreach (var file in files)
                {
                    documentUrl = _documentsService.UploadDocuments(file, listName, null, GenerateRandom());
                    await AppendDocuments(file, profileVersion, listName, documentUrl);
                }
            }

        }
        public string GenerateRandom()
        {
            string tick = DateTime.Now.Ticks.ToString();
            return tick.Substring(tick.Length - 7);
        }
        public async Task<bool> AppendDocuments(IFormFile file, ProfileVersion profileVersion, string documentType, string documentUrl)
        {
            var documentTypeEntity = await _documentTypeRepository.GetAll(d => d.Name == documentType).FirstOrDefaultAsync();
            if (file is not null && documentUrl is not null)
            {
                var str = file.FileName.Split('.', 6);
                Document document = new Document()
                {
                    ProfileVersion = profileVersion,
                    DisplayTitle = str[0],
                    ContentType = file.ContentType,
                    Size = file.Length,
                    Extention = str[str.Length - 1],
                    DocumentUrl = documentUrl,
                    DocumenyType = documentTypeEntity,

                };
                _documentRepository.Add(document);
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool AppendOldDocuments(string oldDocumentsStr, ProfileVersion profileVersion)
        {
            if (oldDocumentsStr is null)
                return false;

            var oldDocuments = JsonConvert.DeserializeObject<List<DocumentDBViewModel>>(oldDocumentsStr);

            if (oldDocuments is not null && oldDocuments.Count > 0)
            {
                foreach (var file in oldDocuments)
                {
                    Document document = new Document()
                    {
                        ProfileVersion = profileVersion,
                        DisplayTitle = file.DisplayTitle,
                        ContentType = file.ContentType,
                        Size = file.Size,
                        Extention = file.Extention,
                        DocumentUrl = file.DocumentUrl,
                        DocumenyTypeId = file.DocumenyTypeId
                    };
                    _documentRepository.Add(document);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
