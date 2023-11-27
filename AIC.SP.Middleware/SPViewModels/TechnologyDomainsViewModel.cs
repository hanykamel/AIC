using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class TechnologyDomainsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.TechnologyDomainsList; }
    }
    public class TechnologyDomainsListViewModel : IListItem<TechnologyDomainsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<TechnologyDomainsViewModel> Items { get; set; }
    }
}
