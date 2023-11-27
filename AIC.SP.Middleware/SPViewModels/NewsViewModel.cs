using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AIC.Data.ViewModels;
using static AIC.CrossCutting.Constant.Constant;
using AIC.SP.Middleware.SPViewModels;

namespace AIC.SP.Middleware
{
    public class NewsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainImage { get; set; }
        public string ThumbnailImage { get; set; }
        public string AICBrief { get; set; }
        public string NewsType { get; set; }
        public string Sector { get; set; }
        public string Technology { get; set; }
        public DateTime PublishDate { get; set; }
        public string Source { get; set; }
        public string SourceUrl { get; set; }
        public string Description { get; set; }
        public List<LookupViewModel> RelatedMediaLinks { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime ArchiveDate { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.News; }
    }
   
}
