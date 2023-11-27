using AIC.Data.ViewModels;
using AIC.Service.Entities.CommonActions.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface IVacancyBusiness
    {
        Task<bool> AddVacancy(AddVacancyCommand command);
        Task<bool> AddUserProfile(AddUserProfileVacancyCommand command);
        Task<VacancyViewModel> GetProfileVacancy(GetVacancyProfileCommand command);

        ListAppliedForCareersListViewModel ListAppliedForCareers(ListAppliedForCareersCommand query);
        VacancyViewModel GetProfileVersionById(GetProfileVersionByIdCommand getProfileVersionByIdCommand);
        bool DeleteById(DeleteVacancyProfileByIdCommand deleteVacancyProfileByIdCommand);
        List<VacancyItemViewModel> GetAll(GetAllVacanciesCommand request);


    }
}
