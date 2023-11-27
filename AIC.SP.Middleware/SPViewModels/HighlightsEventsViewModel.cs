using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class HighlightsEventsViewModel : HighlightsBaseViewModel
    {
        public HighlightsEventsViewModel()
        {
            this.ListName = ListsNames.EventsList;
        }
    }
    public class HighlightsEventsListViewModels : IListItem<HighlightsEventsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<HighlightsEventsViewModel> Items { get; set; }
    }
}
