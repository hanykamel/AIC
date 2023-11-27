using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.SPViewModels
{
    public class ContactUsFormsBaseViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string WorkEducationalOrganization { get; set; }
        public string Message { get; set; }
        public string Reply { get; set; }
        public bool SendEmail { get; set; }
        public bool IsReplySent { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get; set; }
    }
}
