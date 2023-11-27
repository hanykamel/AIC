using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Interfaces;
using AIC.CrossCutting.MailService;
using AIC.CrossCutting.Models.MvcModels;
using AIC.Data.Enums;
using AIC.Data.Model;
using AIC.Data.ViewModels;
using AIC.Repository;
using AIC.Service.Entities.CommonActions.InternShips;
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
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Service.Implementation
{
    public class InternShipsBusiness : IInternShipsBusiness
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
        readonly private IRepository<Internship, int> _internshipRepository;
        private readonly IMapper _mapper;
        private ISendEmail _sendEmailService;
        private readonly IConfiguration _config;
        private readonly ICustomCryptography _crypt;
        readonly private IService<DocumentsViewModel> _documentsService;
        readonly private IUnitOfWork _unitOfWork;
        private readonly IService<InternshipsViewModel> _internshipsService;
        private readonly IRepository<ProfileInternship, Guid> _profileInternshipRepository;
        private string _lang { get; set; }
        #endregion
        #region Ctor
        public InternShipsBusiness(IRepository<ProfileVersion, Guid> profileVersionRepository,
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
                              IRepository<DocumentType, int> documentTypeRepository,
                              IRepository<Internship, int> internshipRepository,
                              IUnitOfWork unitOfWork,
                              IService<InternshipsViewModel> internshipsService,
                              IRepository<ProfileInternship, Guid> profileInternshipRepository)
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
            _internshipsService = internshipsService;
            _profileInternshipRepository = profileInternshipRepository;
            _internshipRepository = internshipRepository;
        }
        #endregion
        #region AIC
        public async Task<bool> AddUserProfile(AddUserProfileInternshipCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("NullModel");
            _lang = command.lang;

            if (!CheckInternshipExpiration(command.InternShipId))
                throw new ModelExpiredException("InternShipExpired");
            var internship = getInternshipById(command.InternShipId);

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
                if (!CheckIfUSerApplyInternshipBefore(userProfile.Id, command.InternShipId))
                    throw new AppliedBeforeException("InternshipAppliedBefore");
            }
            //Send Submittion Form URL
            submittionFormUrl = _config.GetValue<string>("AngularComponents:InternshipsUrl").Replace("{email}", _crypt.Encrypt(command.Email));
            var expirationDate = SetExpirationDate(DateTime.Now);
            submittionFormUrl = submittionFormUrl.Replace("{date}", (_crypt.Encrypt(expirationDate.ToString())));
            submittionFormUrl = submittionFormUrl.Replace("{internshipId}", (_crypt.Encrypt(command.InternShipId.ToString())));
            tempPlaceholder.Add("#FormURLEN", submittionFormUrl + "en");
            tempPlaceholder.Add("#FormURLAR", submittionFormUrl + "ar");
            tempPlaceholder.Add("#NameEn#", internship.Title);
            tempPlaceholder.Add("#NameAr#", internship.TitleAr);
            tempPlaceholder.Add("#InternRefNumEn#", internship.ReferenceNumber);
            tempPlaceholder.Add("#InternRefNumAr#", internship.ReferenceNumber);
            SendMail(toAddresses, "Careers And Internships", MailTypesEnum.AICCareersAndInternships, tempPlaceholder);
            return true;
        }
        public async Task<InternshipProfileViewModel> GetProfileInternship(GetInternshipProfileCommand command)
        {
            if (command.Email is null || command.Date is null || command.InternshipId == "null")
                throw new ArgumentNullException("NullModel");
            _lang = command.lang;
            InternshipViewModelDB internshipsVM = new InternshipViewModelDB();
            var decryptedEmail = _crypt.Decrypt(command.Email);
            var decryptedDate = _crypt.Decrypt(command.Date);
            var decryptedInternshipId = _crypt.Decrypt(command.InternshipId);

            var profileEntity = _profileRepository.GetAll(p => p.Email == decryptedEmail).Include(p => p.ProfileInternships).FirstOrDefault();
            if (profileEntity is null)
                throw new ArgumentNullException("NullModel");

            if (!CheckInternshipExpiration(Int32.Parse(decryptedInternshipId))) // Check Internship Expiration
                throw new ModelExpiredException("InternshipExpired");

            if (!ValidateFormUrl(Convert.ToDateTime(decryptedDate)))     // Check Form Url Expiration
                throw new FormURLNotValidException("FormValidationError");

            if (!CheckIfUSerApplyInternshipBefore(profileEntity.Id, int.Parse(decryptedInternshipId))) // check if user applied before
                throw new AppliedBeforeException("VacancyAppliedBefore");

            var internship = getInternshipById(Int32.Parse(decryptedInternshipId));
            if (internship is not null)
            {
                internshipsVM = _mapper.Map<InternshipViewModelDB>(internship);
            }
            else
            {
                throw new ArgumentNullException("NullModel");
            }
           
            var profileVersionEntity = _profileVersionRepository.GetAll(i => i.ProfileId == profileEntity.Id).Include(i => i.Profile)
                 .OrderByDescending(i => i.Created).FirstOrDefault();
            if (profileVersionEntity is not null)
            {
                var internshipViewModel = _mapper.Map<InternshipProfileViewModel>(profileVersionEntity);

                internshipViewModel.AcademicDegrees =
            _profileDegreeRepository.GetAll(i => i.ProfileVersionId == profileVersionEntity.Id).Include(i => i.Degree).AsEnumerable()
               .Select(item => new AcademicDegreeViewModel()
               {
                   DegreeLevelId = item.Degree.DegreeLevelId,
                   DegreeDate = item.Degree.DegreeDate == DateTime.MinValue ? " " : item.Degree.DegreeDate.Date.ToString("yyyy-MM-dd"),
                   Specialization = item.Degree.Specialization,
                   University = item.Degree.University,
                   InProgressBool = item.Degree.InProgress
               }).ToList();
                internshipViewModel.Certificates =
                    _profileCertification.GetAll(p => p.ProfileVersionId == profileVersionEntity.Id).Include(p => p.Certification).AsEnumerable()
                    .Select(item => new CertificatesViewModel()
                    {
                        CertificateName = item.Certification.CertificateName,
                        CertifiedDate = item.Certification.CertificateDate == DateTime.MinValue ? " " : item.Certification.CertificateDate.Date.ToString("yyyy-MM-dd"),
                        CertifiedFrom = item.Certification.CertifiedFrom
                    }).ToList();

                internshipViewModel.TechnicalSkills =
                    _profileTechnicalSkillsRepository.GetAll(t => t.ProfileVersionId == profileVersionEntity.Id).Include(t => t.TechnicalSkills).AsEnumerable()
                    .Select(item => new TechnicalSkillsViewModel()
                    {
                        SkillName = item.TechnicalSkills.TechnicalSkill == "null" ? " " : item.TechnicalSkills.TechnicalSkill,
                        YearsOfExperience = item.TechnicalSkills.YearsOfExperience
                    }).ToList();

                internshipViewModel.Documents =
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
                internshipViewModel.InternshipsViewModel = internshipsVM;
                var mappedInternshipsViewModel = MapCheckboxToUI(internshipViewModel);

                return mappedInternshipsViewModel;
            }
            return new InternshipProfileViewModel
            {
                Email = decryptedEmail,
                ProfileId = profileEntity.Id.ToString(),
                InternshipsViewModel = internshipsVM
            };

        }
        public async Task<bool> AddInternShips(AddIntershipsCommand command)
        {
            Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
            List<EmailAddress> toAddresses = new List<EmailAddress>();
            if (command == null)
                throw new ArgumentNullException("NullModel");
            var internshipId = _crypt.Decrypt(command.InternshipId);
            var internship = getInternshipById(int.Parse(internshipId));

            var profileVersionEntity = _mapper.Map<ProfileVersion>(command);
            AddProfileVersions(profileVersionEntity);
            AddProfileInternships(profileVersionEntity, Int32.Parse(internshipId));
            AddAcademicDegree(command.AcademicDegreesStr, profileVersionEntity);
            AddCertificates(command.CertificatesStr, profileVersionEntity);
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
            tempPlaceholder.Add("#FormTitleEn#", FormTypes.InternShipFormEn);
            tempPlaceholder.Add("#FormTitleAr#", FormTypes.InternShipFormAr);
            tempPlaceholder.Add("#NameEn#", internship.Title);
            tempPlaceholder.Add("#NameAr#", internship.TitleAr);
            tempPlaceholder.Add("#InternRefNumEn#", command.RefrenceNum);
            tempPlaceholder.Add("#InternRefNumAr#", command.RefrenceNum);
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
        public bool AddProfileInternships(ProfileVersion profileVersionsEntity, int internshipId)
        {
            if (profileVersionsEntity == null || internshipId <= 0)
                throw new ArgumentNullException("NullModel");

            if (!CheckIfUSerApplyInternshipBefore(profileVersionsEntity.ProfileId, internshipId))
                throw new AppliedBeforeException("InternshipAppliedBefore");

            ProfileInternship profileInternship = new ProfileInternship()
            {
                InternshipId = internshipId,
                ProfileId = profileVersionsEntity.ProfileId,
                ProfileVersionId = profileVersionsEntity.Id,
                AppliedDate = DateTime.Now
            };
            _profileInternshipRepository.Add(profileInternship);
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
                    DegreeDate = Convert.ToDateTime(degree.DegreeDate),
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
                    CertificateDate = Convert.ToDateTime(certificate.CertifiedDate),
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
        public bool AddTechnicalSkills(string technicalSkillsStr, ProfileVersion profileVersionsEntity)
        {

            if (technicalSkillsStr == null)
                return false;

            var technicalSkills = JsonConvert.DeserializeObject<List<TechnicalSkillsViewModel>>(technicalSkillsStr);

            foreach (var skill in technicalSkills)
            {
                TechnicalSkills model = new TechnicalSkills()
                {
                    TechnicalSkill = skill.SkillName,
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

        public AppliedForInternshipsListViewModel ListAppliedForInternships(ListAppliedForInternshipsCommand query)
        {
            AppliedForInternshipsListViewModel result = new AppliedForInternshipsListViewModel();
            ListFilter emailFilter = null;
            ListFilter appliedDateFromFilter = null;
            ListFilter appliedDateToFilter = null;
            ListFilter internshipIdFilter = null;
            ListFilter universityFilter = null;
            ListFilter ageFilter = null;
            ListFilter refNum = null;
            if (query.Filters is not null && query.Filters.Count() > 0)
            {
                emailFilter = query.Filters.Where(t => t.Field == "Email").FirstOrDefault();
                appliedDateFromFilter = query.Filters.Where(t => t.Field == "AppliedDateFrom").FirstOrDefault();
                appliedDateToFilter = query.Filters.Where(t => t.Field == "AppliedDateTo").FirstOrDefault();
                internshipIdFilter = query.Filters.Where(t => t.Field == "InternshipId").FirstOrDefault();
                universityFilter = query.Filters.Where(t => t.Field == "University").FirstOrDefault();
                ageFilter = query.Filters.Where(t => t.Field == "Age").FirstOrDefault();
                refNum = query.Filters.Where(t => t.Field == "RefNum").FirstOrDefault();
            }
            var res = _profileInternshipRepository.GetAll()
                .Include(r => r.ProfileVersion).ThenInclude(v => v.ProfileDegrees).ThenInclude(p => p.Degree)
                .Include(r => r.ProfileVersion).ThenInclude(v => v.Documents)
                .Include(r => r.Internship)
                .Include(r => r.Profile)
                .Where(
                t => ((emailFilter != null && !string.IsNullOrEmpty(emailFilter.Value)) ? t.Profile.Email.ToLower().Contains(emailFilter.Value.ToLower()) : true) &&
                ((appliedDateFromFilter != null && !string.IsNullOrEmpty(appliedDateFromFilter.Value)) ? DateTime.Parse(appliedDateFromFilter.Value) <= t.AppliedDate : true) &&
                ((appliedDateToFilter != null && !string.IsNullOrEmpty(appliedDateToFilter.Value)) ? DateTime.Parse(appliedDateToFilter.Value) >= t.AppliedDate : true) &&
                ((internshipIdFilter != null && !string.IsNullOrEmpty(internshipIdFilter.Value) && internshipIdFilter.Value != "-1") ? t.InternshipId.ToString() ==  internshipIdFilter.Value : true) &&
                ((refNum != null && !string.IsNullOrEmpty(refNum.Value)) ? t.Internship.ReferenceNumber == refNum.Value : true) &&
                ((universityFilter != null && !string.IsNullOrEmpty(universityFilter.Value)) ? t.ProfileVersion.ProfileDegrees.Any(d => d.Degree.University.Contains(universityFilter.Value)) : true)&&
                ((ageFilter != null && !string.IsNullOrEmpty(ageFilter.Value) && t.ProfileVersion.BirthDate.Date <= DateTime.Now.Date.AddYears(-int.Parse(ageFilter.Value))) ? (DateTime.Now.Date.Year - t.ProfileVersion.BirthDate.Year) == int.Parse(ageFilter.Value) : true) &&
                ((ageFilter != null && !string.IsNullOrEmpty(ageFilter.Value) && t.ProfileVersion.BirthDate.Date > DateTime.Now.Date.AddYears(-int.Parse(ageFilter.Value))) ? (DateTime.Now.Date.Year - t.ProfileVersion.BirthDate.Year -1) == int.Parse(ageFilter.Value) : true)
                ).ToList();
            var pagedResult = res.Skip(query.PageSize * query.PageIndex).Take(query.PageSize).ToList();
            result.TotalCount = res.Count();
            result.List = _mapper.Map<List<AppliedForInternshipsViewModel>>(pagedResult);
            return result;
        }

        public InternshipProfileViewModel GetInternshipProfileById(GetInternshipProfileByIdCommand getInternshipProfileByIdCommand)
        {
            var profileVersion = _profileVersionRepository.GetAll(p => p.Id == getInternshipProfileByIdCommand.Id)
                .Include(p => p.Profile)
                .Include(p => p.ProfileInternships)
                .Include(p => p.ProfileDegrees).ThenInclude(d => d.Degree).ThenInclude(d => d.DegreeLevel)
                .Include(p => p.ProfileCertifications).ThenInclude(d => d.Certification)
                .Include(p => p.ProfileTechnicalSkills).ThenInclude(d => d.TechnicalSkills)
                .Include(p => p.Documents).ThenInclude(d => d.DocumenyType)
                .FirstOrDefault();
            if (profileVersion is null)
                throw new NotFoundException("ProfileVersionNotFound");
            var result = _mapper.Map<InternshipProfileViewModel>(profileVersion);
            result.AcademicDegrees = _mapper.Map<List<AcademicDegreeViewModel>>(profileVersion.ProfileDegrees);
            result.Certificates = _mapper.Map<List<CertificatesViewModel>>(profileVersion.ProfileCertifications);
            result.TechnicalSkills = _mapper.Map<List<TechnicalSkillsViewModel>>(profileVersion.ProfileTechnicalSkills);
            result.Documents = _mapper.Map<List<DocumentDBViewModel>>(profileVersion.Documents);
            var internshipId = profileVersion.ProfileInternships.FirstOrDefault()?.InternshipId;
            if (internshipId is not null)
            {
                InternshipsViewModel internship;
                try
                {
                    internship = _internshipsService.GetById("", internshipId.Value);

                }
                catch (NotFoundException ex)
                {
                    
                   internship = null;

                }
                if (internship is not null)
                    result.InternshipsViewModel = _mapper.Map<InternshipViewModelDB>(internship);
            }

            return result;
        }
        public bool DeleteById(DeleteInternshipProfileByIdCommand deleteInternshipProfileByIdCommand)
        {
            var profileVersion = _profileVersionRepository.GetAll(p => p.Id == deleteInternshipProfileByIdCommand.Id)
                .Include(p => p.ProfileInternships)
                .FirstOrDefault();
            if (profileVersion is null)
                throw new NotFoundException("ProfileVersionNotFound");
            profileVersion.IsDeleted = true;
            profileVersion.ProfileInternships.FirstOrDefault().IsDeleted = true;
            _profileVersionRepository.Update(profileVersion);
            _unitOfWork.SaveChanges();
            return true;
        }

        public List<InternshipItemViewModel> GetAll(GetAllInternshipsCommand request)
        {
            var items = _internshipRepository.GetAll();
            var result = _mapper.Map<List<InternshipItemViewModel>>(items);
            return result;
        }
        #endregion
        #region Helper
        public bool CheckInternshipExpiration(int internShipId)
        {
            if (internShipId > 0)
            {
                var internship = getInternshipById(internShipId);
                if (internship is not null && internship.ExpiryDate != DateTime.MinValue)
                    return ValidateFormUrl(internship.ExpiryDate);
                else
                    return false;
            }
            return false;
        }
        public InternshipsViewModel getInternshipById(int internShipId)
        {
            if (internShipId <= 0)
                return null;

            try
            {
                var internship = _internshipsService.GetById("", internShipId);
                return internship;
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
        public bool CheckIfUSerApplyInternshipBefore(Guid profileId, int internshipId)
        {
            var profileInternShip = _profileInternshipRepository.GetAll(i => i.ProfileId == profileId
                                                          && i.InternshipId == internshipId).FirstOrDefault();
            if (profileInternShip is null)
                return true;    // not applied before
            else
                return false;   // applied before

        }
        public DateTime SetExpirationDate(DateTime date)
        {
            return date.AddHours(12);
        }
        public void SendMail(List<EmailAddress> toAddresses, string mailSubject, MailTypesEnum mailType, Dictionary<string, string> data, string errorMsg = null)
        {
            _sendEmailService.SendMail(toAddresses, mailSubject, mailType, data);
        }
        public InternshipProfileViewModel MapCheckboxToUI(InternshipProfileViewModel internshipViewModel)
        {
            if (internshipViewModel is null)
                return null;

            if (internshipViewModel.AcademicDegrees is not null)
            {

                foreach (var internship in internshipViewModel.AcademicDegrees)
                {
                    if (internship.InProgressBool == true)
                    {
                        string str = " ";
                        List<string> strLst = new List<string>();
                        strLst.Add(str);
                        internship.InProgress = strLst;
                    }
                }
            }
            if (internshipViewModel.JoinedUsAs != null)
            {
                List<string> joinedUsLst = new List<string>();
                joinedUsLst.Add(internshipViewModel.JoinedUsAs);
                internshipViewModel.JoinedUsAsLst = joinedUsLst;
            }
            return internshipViewModel;
        }
        public async Task UploadDocuments(IFormFileCollection files, string listName, ProfileVersion profileVersion)
        {
            if (files != null && files.Count > 0)
            {
                string documentUrl;

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
