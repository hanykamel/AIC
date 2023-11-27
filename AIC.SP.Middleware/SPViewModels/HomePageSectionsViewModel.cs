using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class HomePageSectionsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FeaturedCareersBrief { get; set; }
        public string ContactUsBrief { get; set; }

        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.HomePageSectionsList; }
    }
    public class HomePageSectionsListViewModel : IListItem<HomePageSectionsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<HomePageSectionsViewModel> Items { get; set; }
    }
}
