using AIC.Data.ViewModels.Lookups;
using AIC.Service.Entities.CommonActions.RequestsLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface IRequestLookupBusiness
    {
       List<LookupsViewModel> ListDegreeLevel(ListDegreeLevelCommand degreeLevelCommand);
        List<LookupsViewModel> ListJobTypes(ListJobTypesCommand jobTypesCommand);

    }
}
