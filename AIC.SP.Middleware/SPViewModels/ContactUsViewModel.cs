using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class ContactUsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string Address { get; set; }
        public string AddressAr { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsHeadOffice { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.ContactUs; }
    }

    public class ContactUsListViewModel : IListItem<ContactUsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<ContactUsViewModel> Items { get; set; }
    }
}
