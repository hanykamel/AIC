using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.SPViewModels
{
    public class CreateContactUsBaseViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int TypeId { get; set; }
        public string WorkEducationalOrganization { get; set; }
        public string Message { get; set; }
        public string RequestNumber { get; set; }
    }
}
