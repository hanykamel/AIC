using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware
{
    public class FAQViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TopicTitle { get; set; }
        public string Answer { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.FAQsList; }
    }

    public class FAQListViewModels : IListItem<FAQViewModel>
    {
        public string PagingInfo { get; set; }
        public List<FAQViewModel> Items { get; set; }
    }
}
