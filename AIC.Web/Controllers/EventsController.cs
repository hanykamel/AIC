using AIC.CrossCutting.Constant;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Web.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IService<EventsViewModel> _eventsService;
        private readonly IService<VideoViewModel> _videoService;
        private readonly IService<PhotoViewModel> _photoService;
        public EventsController(IService<EventsViewModel> eventsService,IService<VideoViewModel> videoService,
            IService<PhotoViewModel> photoService)
        {
            _eventsService = eventsService;
            _videoService = videoService;
            _photoService = photoService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.IsActive,
                Operator = "Geq",
                Value = "<Today />",
                FieldValueType = "DateTime"
            });
            var items = _eventsService.GetAll(query);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetById(int id)
        {
            string ListName;
            var item = _eventsService.GetById(_lang, id);
            if (!string.IsNullOrEmpty(item.EventImages))
            {
                ListName = ListsNames.PhotoGallery + "/" + item.EventImages;
                var photos = _photoService.GetFromAlbums(new Query { Lang = _lang }, ListName);
                item.EventImagesList = photos.Items;
            }
            if (!string.IsNullOrEmpty(item.EventVideos))
            {
                ListName = ListsNames.VideoGallery + "/" + item.EventVideos;
                var videos = _videoService.GetFromAlbums(new Query { Lang = _lang }, ListName);
                item.EventVideosList = videos.Items;
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> GetArchivedList([FromBody] Query query)
        {
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.IsActive,
                Operator = "Lt",
                Value = "<Today />",
                FieldValueType = "DateTime"
            });
            var items = _eventsService.GetAll(query);
            return Ok(items);
        }
    }
}
