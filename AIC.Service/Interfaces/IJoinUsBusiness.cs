using AIC.Data.ViewModels;
using AIC.Service.Entities.CommonActions.Join_Us;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface IJoinUsBusiness
    {
        Task<bool> AddJoinUs(AddJoinUsCommand comand);
        Task<JoinUsViewModel> GetJoinUs(string email , string date);
        Task<List<AddJoinUsCommand>> List();
        Task<bool> AddUserProfile(string Email);
        JoinUsApplicationListViewModel ListJoinUs(ListJoinUsCommand listJoinUsCommand);
        JoinUsViewModel GetJoinUsById(GetJoinUsByIdCommand getJoinUsByIdCommand);
        bool DeleteById(DeleteJoinUsByIdCommand deleteJoinUsByIdCommand);
    }
}
