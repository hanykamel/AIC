using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class ProjectsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool ShowInHomePage { get; set; }
        public string ApplicationDomain { get; set; }
        public string TechnologyDomain { get; set; }
        public string URL { get; set; }
        public List<LookupViewModel> RelatedMediaLinks { get; set; }
        public string DemoPhotos { get; set; }
        public List<PhotoViewModel> DemoPhotosList { get; set; }
        public string DemoVideos { get; set; }
        public List<VideoViewModel> DemoVideosList { get; set; }
        public string DemoBrief { get; set; }
        public string ImagesTitle { get; set; }
        public string VideosTitle { get; set; }
        public string MainImage { get; set; }
        public string ThumbnailImage { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.ProjectsList; }
    }

    public class ProjectsListViewModel : IListItem<ProjectsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<ProjectsViewModel> Items { get; set; }
    }
}
