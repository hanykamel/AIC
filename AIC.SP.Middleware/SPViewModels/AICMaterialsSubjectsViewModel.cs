using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class AICMaterialsSubjectsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoAlbum { get; set; }
        public List<VideoViewModel> VideosList { get; set; }
        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.AICMaterialsSubjectsList; }
    }

    public class AICMaterialsSubjectsListViewModel : IListItem<AICMaterialsSubjectsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<AICMaterialsSubjectsViewModel> Items { get; set; }
    }
}
