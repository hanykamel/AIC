using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class BestPracticeViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Description { get; set; }
        public string PhotoAlbum { get; set; }
        public List<PhotoViewModel> PhotoList { get; set; }
        public string VideoAlbum { get; set; }
        public List<VideoViewModel> VideosList { get; set; }
        public DateTime PractiseDate { get; set; }

        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.BestPracticesList; }
    }

    public class BestPracticesListViewModel : IListItem<BestPracticeViewModel>
    {
        public string PagingInfo { get; set; }
        public List<BestPracticeViewModel> Items { get; set; }
    }
}
