using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Web.Controllers
{

    public class AICMaterialsController : BaseController
    {
        private readonly IService<AICMaterialsSubjectsViewModel> _aicMaterialsSubjectsService;
        private readonly IService<AICMaterialsViewModel> _aicMaterialsService;
        private readonly IService<VideoViewModel> _videoService;
        private readonly IService<AICMaterialsVideosViewModel> _aicMaterialsVideosService;

        public AICMaterialsController(IService<AICMaterialsSubjectsViewModel> aicMaterialsSubjectsService,
            IService<VideoViewModel> videoService, IService<AICMaterialsViewModel> aicMaterialsService,
            IService<AICMaterialsVideosViewModel> aicMaterialsVideosService)
        {
            _aicMaterialsSubjectsService = aicMaterialsSubjectsService;
            _videoService = videoService;
            _aicMaterialsService = aicMaterialsService;
            _aicMaterialsVideosService = aicMaterialsVideosService;
        }
        [HttpPost]
        public ActionResult<HttpResponseMessage> ListAICMaterials([FromBody] Query query)
        {
            var materials = _aicMaterialsService.GetAll(query);
            foreach (var material in materials.Items)
            {
                if (!string.IsNullOrEmpty(material.VideoId))
                {
                    var q = new Query
                    {
                        Lang = _lang,
                        Filters = new List<Filter>()
                    };
                    q.Filters.Add(new Filter
                    {
                        Field = Fields.ID,
                        FieldValueType = "Integer",
                        Operator = "Eq",
                        Value = material.VideoId
                    });
                    var videos = _aicMaterialsVideosService.GetAllDocuments(q);
                    material.Video = videos.Items.FirstOrDefault();
                }
            }
            return Ok(materials);
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> ListMaterials()
        {
            string ListName;
            var items = _aicMaterialsSubjectsService.GetAll(new Query { Lang = _lang });
            foreach (var item in items.Items)
            {
                if (!string.IsNullOrEmpty(item.VideoAlbum))
                {
                    ListName = ListsNames.EducationalVideos + "/" + item.VideoAlbum;
                    var videos = _videoService.GetFromAlbums(new Query { Lang = _lang }, ListName);
                    item.VideosList = videos.Items;
                }
            }
            
            var result = from itms in items.Items
                         select new { Id = itms.Id, Title = itms.Title, VideosList = itms.VideosList };
            return Ok(result);
        }

        
    }
}
