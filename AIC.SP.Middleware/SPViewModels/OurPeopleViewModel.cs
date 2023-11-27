using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class OurPeopleViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string AICBrief { get; set; }
        public string AICImage { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.OurPeopleList; }
    }
}
