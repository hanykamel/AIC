using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class EventsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainImage { get; set; }
        public string ThumbnailImage { get; set; }
        public string AICBrief { get; set; }
        public string EventType { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Description { get; set; }
        public string EventImages { get; set; }
        public List<PhotoViewModel> EventImagesList { get; set; }
        public string EventVideos { get; set; }
        public List<VideoViewModel> EventVideosList { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime ArchiveDate { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.Events; }
    }
}
