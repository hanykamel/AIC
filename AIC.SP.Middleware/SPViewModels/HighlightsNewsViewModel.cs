using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public  class HighlightsNewsViewModel : HighlightsBaseViewModel
    {
        public HighlightsNewsViewModel()
        {
            this.ListName = ListsNames.NewsList;
        }
    }
    public class HighlightsNewsListViewModels : IListItem<HighlightsNewsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<HighlightsNewsViewModel> Items { get; set; }
    }
}
