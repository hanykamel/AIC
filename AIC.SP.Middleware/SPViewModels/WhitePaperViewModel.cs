using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class WhitePaperViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.WhitePapersList; }
    }

    public class WhitePapersListViewModel : IListItem<WhitePaperViewModel>
    {
        public string PagingInfo { get; set; }
        public List<WhitePaperViewModel> Items { get; set; }
    }
}
