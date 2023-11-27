using AIC.CrossCutting.ExceptionHandling;
using AIC.Data.Enums;
using AIC.Data.Model;
using AIC.Data.ViewModels;
using AIC.Repository;
using AIC.Service.Entities.CommonActions.Synchronization;
using AIC.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Implementation
{
    public class SynchronizationService : ISynchronizationService
    {
        readonly private IRepository<Vacancy, int> _vacancyRepository;
        readonly private IRepository<DegreeLevel, int> _degreeLevelRepository;
        readonly private IRepository<JobTypes, int> _jobTypesRepository;
        readonly private IRepository<Internship, int> _internshipRepository;
        readonly private IUnitOfWork _unitOfWork;
        ILogger<SynchronizationService> _logger;

        public SynchronizationService(IRepository<Vacancy, int> vacancyRepository,
            IRepository<DegreeLevel, int> degreeLevelRepository, IRepository<JobTypes, int> jobTypesRepository,
            IRepository<Internship, int> internshipRepository, IUnitOfWork unitOfWork,
            ILogger<SynchronizationService> logger)
        {
            _unitOfWork = unitOfWork;
            _vacancyRepository = vacancyRepository;
            _degreeLevelRepository = degreeLevelRepository;
            _jobTypesRepository = jobTypesRepository;
            _internshipRepository = internshipRepository;
            _logger = logger;
        }
        public async Task<bool> Sychronize(SychronizeCommand model)
        {
            switch (model.Type)
            {
                case SynchronizationEnum.Vacancies:
                    return await VacanciesSychronize(model);
                case SynchronizationEnum.Internships:
                    return await InternshipsSychronize(model);
                case SynchronizationEnum.DegreeLevels:
                    return await DegreeLevelsSychronize(model);
                case SynchronizationEnum.JobTypes:
                    return await JobTypesSychronize(model);
                default:
                    return false;
            }
        }

        public async Task<bool> JobTypesSychronize(SychronizeCommand model)
        {
            _logger.LogInformation("Inside JobTypesSychronize");
            if (model.IsDeleted)
            {
                _logger.LogInformation("Add jop type");
                return await DeleteJobType(model.Id, model.IsDeleted);
            }
            var jobType = _jobTypesRepository.GetFirst(v => v.Id == model.Id);

            if (jobType == null)
            {
                _logger.LogInformation("Add Jop type");
                await _jobTypesRepository.AddAsync(new JobTypes
                { TitleEn = model.Title, TitleAr = model.TitleAr, Id = model.Id, IsDeleted = model.IsDeleted });

            }
            else
            {
                _logger.LogInformation("update Jop type");
                jobType.TitleEn = !string.IsNullOrEmpty(model.Title) ? model.Title : jobType.TitleEn;
                jobType.TitleAr = !string.IsNullOrEmpty(model.TitleAr) ? model.TitleAr : jobType.TitleAr;
                jobType.IsDeleted = model.IsDeleted;

                _jobTypesRepository.Update(jobType);
            }

            _logger.LogInformation("Delete JobTypesSychronize");
            return await _unitOfWork.SaveChangesAsync() > 0;

        }
        public async Task<bool> DegreeLevelsSychronize(SychronizeCommand model)
        {
            _logger.LogInformation("Inside DegreeLevelsSychronize");
            if (model.IsDeleted)
            {
                _logger.LogInformation("Delete Degree level");
                return await DeleteDegreeLevel(model.Id, model.IsDeleted);
            }
            var degreeLevel = _degreeLevelRepository.GetFirst(v => v.Id == model.Id);
            if (degreeLevel == null)
            {
                _logger.LogInformation("Add Degree level");
                await _degreeLevelRepository.AddAsync(new DegreeLevel
                { TitleEn = model.Title, TitleAr = model.TitleAr, Id = model.Id, IsDeleted = model.IsDeleted });

            }
            else
            {
                _logger.LogInformation("update Degree level");
                degreeLevel.TitleEn = !string.IsNullOrEmpty(model.Title) ? model.Title : degreeLevel.TitleEn;
                degreeLevel.TitleAr = !string.IsNullOrEmpty(model.TitleAr) ? model.TitleAr : degreeLevel.TitleAr;
                degreeLevel.IsDeleted = model.IsDeleted;

                _degreeLevelRepository.Update(degreeLevel);
            }
            _logger.LogInformation("End DegreeLevelsSychronize");
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> InternshipsSychronize(SychronizeCommand model)
        {
            _logger.LogInformation("inside InternshipsSychronize");
            if (model.IsDeleted)
            {
                _logger.LogInformation("Delete internship");
                return await DeleteInternship(model.Id, model.IsDeleted);
            }
            var internship = _internshipRepository.GetFirst(v => v.Id == model.Id);
            if (internship == null)
            {
                _logger.LogInformation("Add internship");
                await _internshipRepository.AddAsync(new Internship
                { NameEn = model.Title, NameAr = model.TitleAr, Id = model.Id, IsDeleted = model.IsDeleted, ReferenceNumber = model.ReferenceNumber });
            }
            else
            {
                _logger.LogInformation("Update internship");
                internship.NameEn = !string.IsNullOrEmpty(model.Title) ? model.Title : internship.NameEn;
                internship.NameAr = !string.IsNullOrEmpty(model.TitleAr) ? model.TitleAr : internship.NameAr;
                internship.IsDeleted = model.IsDeleted;
                internship.ReferenceNumber = !string.IsNullOrEmpty(model.ReferenceNumber) ? model.ReferenceNumber : internship.ReferenceNumber;
                _internshipRepository.Update(internship);
            }
            _logger.LogInformation("End InternshipsSychronize");
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> VacanciesSychronize(SychronizeCommand model)
        {
            _logger.LogInformation("inside VacanciesSychronize");
            if (model.IsDeleted)
            {
                _logger.LogInformation("Delete vacancy");
                return await DeleteVacancy(model.Id, model.IsDeleted);
            }
            var vacancy = _vacancyRepository.GetFirst(v => v.Id == model.Id);
            if (vacancy == null)
            {
                _logger.LogInformation("Add vacancy");
                await _vacancyRepository.AddAsync(new Vacancy
                { NameEn = model.Title, NameAr = model.TitleAr, Id = model.Id, IsDeleted = model.IsDeleted, ReferenceNumber = model.ReferenceNumber });

            }
            else
            {
                _logger.LogInformation("update vacancy");
                vacancy.NameEn = !string.IsNullOrEmpty(model.Title) ? model.Title : vacancy.NameEn;
                vacancy.NameAr = !string.IsNullOrEmpty(model.TitleAr) ? model.TitleAr : vacancy.NameAr;
                vacancy.IsDeleted = model.IsDeleted;
                vacancy.ReferenceNumber = !string.IsNullOrEmpty(model.ReferenceNumber) ? model.ReferenceNumber : vacancy.ReferenceNumber;
                _vacancyRepository.Update(vacancy);
            }

            _logger.LogInformation("End VacanciesSychronize");
            return await _unitOfWork.SaveChangesAsync() > 0;


        }


        public async Task<bool> DeleteVacancy(int id, bool delete)
        {
            var vacancy = _vacancyRepository.GetAll(v => v.Id == id, true).Include(v => v.ProfileVacancies).FirstOrDefault();
            if (delete && vacancy.ProfileVacancies is not null && vacancy.ProfileVacancies.Count() > 0)
            {
                throw new PreConditionFailedException("CannotDeleteVacancy");
            }
            vacancy.IsDeleted = delete;
            _vacancyRepository.LogicalDelete(vacancy);
            return await _unitOfWork.SaveChangesAsync() > 0;

        }
        public async Task<bool> DeleteDegreeLevel(int id, bool delete)
        {
            var degreeLevel = _degreeLevelRepository.GetAll(v => v.Id == id, true).Include(v => v.Degrees).FirstOrDefault();
            if (delete && degreeLevel.Degrees is not null && degreeLevel.Degrees.Count() > 0)
            {
                throw new PreConditionFailedException("CannotDeleteDegreeLevel");
            }
            _degreeLevelRepository.LogicalDelete(degreeLevel);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteJobType(int id, bool delete)
        {
            var jobType = _jobTypesRepository.GetAll(v => v.Id == id, true).Include(v => v.WorkExperiences).FirstOrDefault();
            if (delete && jobType.WorkExperiences is not null && jobType.WorkExperiences.Count() > 0)
            {
                throw new PreConditionFailedException("CannotDeleteJobType");
            }
            jobType.IsDeleted = delete;
            _jobTypesRepository.Update(jobType);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInternship(int id, bool delete)
        {
            var internship = _internshipRepository.GetAll(v => v.Id == id, true).Include(v => v.ProfileInternships).FirstOrDefault();
            if (delete && internship.ProfileInternships is not null && internship.ProfileInternships.Count() > 0)
            {
                throw new PreConditionFailedException("CannotDeleteInternship");
            }
            _internshipRepository.LogicalDelete(internship);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
