using AIC.Data.ViewModels;
using AIC.Service.Entities.CommonActions.InternShips;
using AIC.Service.Entities.CommonActions.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface IInternShipsBusiness
    {
        Task<bool> AddUserProfile(AddUserProfileInternshipCommand command);
        Task<InternshipProfileViewModel> GetProfileInternship(GetInternshipProfileCommand command);
        Task<bool> AddInternShips(AddIntershipsCommand command);
        AppliedForInternshipsListViewModel ListAppliedForInternships(ListAppliedForInternshipsCommand query);
        InternshipProfileViewModel GetInternshipProfileById(GetInternshipProfileByIdCommand getInternshipProfileByIdCommand);
        bool DeleteById(DeleteInternshipProfileByIdCommand deleteInternshipProfileByIdCommand);
        List<InternshipItemViewModel> GetAll(GetAllInternshipsCommand request);

    }
}
