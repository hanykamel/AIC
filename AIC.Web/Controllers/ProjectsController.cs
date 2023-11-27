using AIC.CrossCutting.ExceptionHandling;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Web.Controllers
{
    
    public class ProjectsController : BaseController
    {
        private readonly IService<ProjectsViewModel> _projectsService;
        private readonly IService<ApplicationsDomainsViewModel> _applicationsDomainsService;
        private readonly IService<TechnologyDomainsViewModel> _technologyDomainsViewModelService;
        private readonly IService<VideoViewModel> _videoService;
        private readonly IService<PhotoViewModel> _photoService;
        public ProjectsController(IService<ProjectsViewModel> projectsService, IService<ApplicationsDomainsViewModel> applicationsDomainsService,
                    IService<TechnologyDomainsViewModel> technologyDomainsViewModelService, 
                    IService<VideoViewModel> videoService, IService<PhotoViewModel> photoService)
        {
            _projectsService = projectsService;
            _applicationsDomainsService = applicationsDomainsService;
            _technologyDomainsViewModelService = technologyDomainsViewModelService;
            _videoService = videoService;
            _photoService = photoService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            var items = _projectsService.GetAll(query);
            return Ok(items);
        }


        [HttpGet]
        public ActionResult<HttpResponseMessage> GetById(int id)
        {
            string ListName;
            var item = _projectsService.GetById(_lang, id);
            if (item == null)
                throw new NotFoundException("Not Found");
            if (!string.IsNullOrEmpty(item.DemoPhotos))
            {
                ListName = ListsNames.PhotoGallery + "/" + item.DemoPhotos;
                var photos = _photoService.GetFromAlbums(new Query { Lang = _lang }, ListName);
                item.DemoPhotosList = photos.Items;
            }
            if (!string.IsNullOrEmpty(item.DemoVideos))
            {
                ListName = ListsNames.VideoGallery + "/" + item.DemoVideos;
                var videos = _videoService.GetFromAlbums(new Query { Lang = _lang }, ListName);
                item.DemoVideosList = videos.Items;
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> ListApplicationDomains([FromBody] Query query)
        {
            query = new Query() { Lang = "" };
            var items = _applicationsDomainsService.GetAll(query);
            return Ok(items);
        }
        [HttpPost]
        public ActionResult<HttpResponseMessage> ListTechnologyDomains([FromBody] Query query)
        {
            query = new Query() { Lang = "" };
            var items = _technologyDomainsViewModelService.GetAll(query);
            return Ok(items);
        }
       
    }
}
