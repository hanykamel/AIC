using AIC.Data.ViewModels;
using AIC.Service.Entities.CommonActions.Synchronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface ISynchronizationService
    {
        Task<bool> Sychronize(SychronizeCommand model);
    }
}
