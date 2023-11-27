using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class AICMaterialsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubjectId { get; set; }
        public string Brief { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string VideoId { get; set; }
        public AICMaterialsVideosViewModel Video { get; set; }
        public string DocumentUrl { get; set; }

        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.AICMaterialsList; }
    }
    public class AICMaterialsListViewModel : IListItem<AICMaterialsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<AICMaterialsViewModel> Items { get; set; }
    }
}
