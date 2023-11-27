using AIC.Data.ViewModels;
using AIC.Service.Entities.CommonActions.Newsletter.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface INewsletterBusiness
    {
         Task<bool>  AddSubscriber(SubscriberViewModel subscriber);
        ListSubscribersViewModel Get(GetAllSubscriberCommand query);
        Task<bool> UnSubscrib(string Email);
        //public bool SendNewsLetter();
         Task<bool> SendNewsLetter();
    }
}
