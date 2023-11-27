using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class ContactUsFormsSuggestionsViewModel : ContactUsFormsBaseViewModel
    {
        public ContactUsFormsSuggestionsViewModel()
        {
            this.ListName = ListsNames.ContactUsSuggestions;
        }
    }
}
