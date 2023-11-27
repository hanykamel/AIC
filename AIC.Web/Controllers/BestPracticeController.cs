using AIC.CrossCutting.ExceptionHandling;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Web.Controllers
{
   
    public class BestPracticeController : BaseController
    {

        private readonly IService<BestPracticeViewModel> _bestPracticesService;
        private readonly IService<VideoViewModel> _videoService;
        private readonly IService<PhotoViewModel> _photoService;
        public BestPracticeController(IService<BestPracticeViewModel> bestPracticesService, IService<VideoViewModel> videoService,
            IService<PhotoViewModel> photoService)
        {
            _bestPracticesService = bestPracticesService;
            _videoService = videoService;
            _photoService = photoService;
        }


        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {
            var items = _bestPracticesService.GetAll(query);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetById(int id)
        {
            string ListName;
            var item = _bestPracticesService.GetById(_lang, id);
            if (item == null)
            {
                throw new NotFoundException("Not Found");
            }
            if(!string.IsNullOrEmpty(item.PhotoAlbum))
            {
                ListName = ListsNames.BestPracticePhotoGallery + "/" + item.PhotoAlbum;
                var photos = _photoService.GetFromAlbums(new Query { Lang=_lang}, ListName);
                item.PhotoList = photos.Items;
            }
            if (!string.IsNullOrEmpty(item.VideoAlbum))
            {
                ListName = ListsNames.BestPracticeVideoGallery + "/" + item.VideoAlbum;
                var videos = _videoService.GetFromAlbums(new Query { Lang = _lang }, ListName);
                item.VideosList = videos.Items;
            }
            return Ok(item);
        }
    }
}
