using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class MailingListViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string BodyAr { get; set; }
        public string Attachments { get; set; }
        public DateTime? Date { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.MailingList; }
    }
}
