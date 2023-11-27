using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class MainMenuViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string Parent { get; set; }
        public bool HasChildren { get; set; }
        public string Url { get; set; }
        public string OtherUrl { get; set; }
        public int Order { get; set; }
        public bool Hidden { get; set; }
        public bool ShowInFooter { get; set; }
        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.MainMenuList; }
    }
    public class MainMenuListViewModel : IListItem<MainMenuViewModel>
    {
        public string PagingInfo { get; set; }
        public List<MainMenuViewModel> Items { get; set; }
    }

    public class MenuItem
    {
        public int Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string Parent { get; set; }
        public bool HasChildren { get; set; }
        public string Url { get; set; }
        public string OtherUrl { get; set; }
        public int Order { get; set; }
        public bool Hidden { get; set; }
        public List<MenuItem> Children { get; set; }
    }
}
