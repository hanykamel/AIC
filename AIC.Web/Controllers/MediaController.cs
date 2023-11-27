using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using Asset.SharePoint.Middleware.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AIC.CrossCutting.ExceptionHandling;
using static AIC.CrossCutting.Constant.Constant;
using AIC.SP.Middleware;

namespace AIC.Web.Controllers
{
    public class MediaController : BaseController
    {
        private readonly IService<PhotoAlbumViewModel> _photoAlbumService;
        private readonly IService<VideoAlbumViewModel> _videoAlbumService;
        private readonly IService<VideoViewModel> _videoService;
        private readonly IService<PhotoViewModel> _photoService;
        private readonly IService<DocumentsViewModel> _documentsService;


        public MediaController(IService<PhotoAlbumViewModel> photoAlbumService, IService<VideoAlbumViewModel> videoAlbumService,
            IService<VideoViewModel> videoService, IService<PhotoViewModel> photoService, IService<DocumentsViewModel> documentsService)
        {
            _photoAlbumService = photoAlbumService;
            _videoService = videoService;
            _videoAlbumService = videoAlbumService;
            _photoService = photoService;
            _documentsService = documentsService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> PhotoAlbumsList([FromBody] Query query)
        {
            query.Filters.Add(new Filter() { Field = "FSObjType", FieldValueType = "Integer", Operator = "Eq", Value = "1" });
            var items = _photoAlbumService.GetAllDocuments(query);
            return Ok(items);
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> PhotoAlbumPhotosList([FromBody] Query query, string albumName)
        {
            string ListName = ListsNames.PhotoGallery + "/" + albumName;
            var items = _photoService.GetFromAlbums(query, ListName);
            return Ok(items);
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> VideoAlbumsList([FromBody] Query query)
        {
            query.Filters.Add(new Filter() { Field = "FSObjType", FieldValueType = "Integer", Operator = "Eq", Value = "1" });
            var items = _videoAlbumService.GetAllDocuments(query); 
            return Ok(items);
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> VideoAlbumVideosList([FromBody] Query query, string albumName)
        {
            string ListName = ListsNames.VideoGallery + "/" + albumName;
            var items = _videoService.GetFromAlbums(query, ListName);
            return Ok(items);
        }

        
        [HttpGet]
        public ActionResult<HttpResponseMessage> GetPhotoAlbumDetails(string albumName)
        {
                var items = _photoAlbumService.GetAlbumDetails(_lang + "/",  albumName);
                return Ok(items);

        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> GetVideoAlbumDetails(string albumName)
        {
            var items = _videoAlbumService.GetAlbumDetails(_lang + "/", albumName);
            return Ok(items);

        }

    }
}
