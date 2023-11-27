using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class BodAndScientificViewModel : IContentItem
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.BODAndScientificCommitteeList; }
        public string AICBrief { get; set; }
        public string AICImage { get; set; }


    }
}
