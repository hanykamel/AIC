
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;
using AIC.CrossCutting.Constant;
using Microsoft.AspNetCore.Authorization;
using AIC.Data.Permissions;
using AIC.SP.Middleware;
using AIC.Web.Filters;
using AIC.CrossCutting.ExceptionHandling;
using AIC.SP.Middleware.SPViewModels;
using AIC.SP.Middleware.Helpers;
using Microsoft.Extensions.Configuration;

namespace AIC.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IService<NewsViewModel> _newsService;
        private readonly IService<HomePageSectionsViewModel> _homePageSectionsService;
        private readonly IService<ProjectsViewModel> _projectsService;
        private readonly IService<HighlightsNewsViewModel> _highlightsNewsService;
        private readonly IService<HighlightsEventsViewModel> _highlightsEventsService;
        private readonly IService<CareersViewModel> _careersService;
        private readonly IService<MainMenuViewModel> _mainMenuService;
        private readonly IService<PortalPagesViewModel> _portalPagesService;

        private readonly IService<MainBannerViewModel> _mainBannerService;
        private readonly IConfiguration _config;


        public HomeController(IService<NewsViewModel> newsService, IService<HomePageSectionsViewModel> homePageSectionsService,
            IService<ProjectsViewModel> projectsService, IService<HighlightsNewsViewModel> highlightsNewsService,
            IService<HighlightsEventsViewModel> highlightsEventsService, IService<CareersViewModel> careersService,
            IService<MainMenuViewModel> mainMenuService, 
            IService<MainBannerViewModel> mainBannerService, IConfiguration config, IService<PortalPagesViewModel> portalPagesService)
        {
            _newsService = newsService;
            _homePageSectionsService = homePageSectionsService;
            _projectsService = projectsService;
            _highlightsNewsService = highlightsNewsService;
            _highlightsEventsService = highlightsEventsService;
            _careersService = careersService;
            _mainMenuService = mainMenuService;
          
            _mainBannerService = mainBannerService;
            _config = config;
            _portalPagesService = portalPagesService;
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetHomeSections()
        {
            var query = new Query
            {
                Lang = _lang,
                SortingField = Constant.Fields.Modified,
                IsSortingAscending = false,
                PageSize = 1
            };
            var item = _homePageSectionsService.GetAll(query).Items.FirstOrDefault();
            if (item == null)
                throw new NotFoundException("Not Found");
            return Ok(item);
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> GetMainMenu()
        {
            var query = new Query
            {
                Filters = new List<Filter>()
            };
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.Hidden,
                Operator = "Eq",
                Value = "0",
                FieldValueType = "Boolean"
            });
            var items = _mainMenuService.GetAll(query).Items;
            var portalPage = _portalPagesService.GetAll(new Query()).Items;
            return Ok(new { Sections = items, Urls = portalPage });
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetProjects()
        {
            var query = new Query() { Lang = _lang };
            query.Filters = new List<Filter>();
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.ShowInHomePage, Operator = "Eq", Value = "1", FieldValueType = "Boolean"
            });
            query.SortingField = Constant.Fields.Modified;
            query.IsSortingAscending = false;
            query.PageSize = 3;
            var items = _projectsService.GetAll(query);
            
            return Ok(items);
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> GetCareers()
        {
            var query = new Query();
            query.Filters = new List<Filter>();
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.ShowInHomePage,
                Operator = "Eq",
                Value = "1",
                FieldValueType = "Boolean"
            });
            query.SortingField = Constant.Fields.Modified;
            query.IsSortingAscending = false;
            query.PageSize = 3;
            var items = _careersService.GetAll(query);
            
            return Ok(items);
        }
        [HttpGet]
        public ActionResult<HttpResponseMessage> GetHighlights()
        {
            var query = new Query() { Lang = _lang };
            query.Filters = new List<Filter>();
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.ShowInHomePage,
                Operator = "Eq",
                Value = "1",
                FieldValueType = "Boolean"
            });
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.IsActive,
                Operator = "Geq",
                Value = "<Today />",
                FieldValueType = "DateTime"
            });
            query.SortingField = Constant.Fields.Modified;
            query.IsSortingAscending = false;
            query.PageSize = 3;
            List<HighlightsBaseViewModel> items = new List<HighlightsBaseViewModel>();
            var news = _highlightsNewsService.GetAll(query);
            var events = _highlightsEventsService.GetAll(query);
            items.AddRange(news.Items);
            items.AddRange(events.Items);
            items.OrderByDescending(i => i.ModifiedDate);
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<HttpResponseMessage> GetMainBanner()
        {
            var query = new Query
            {
                Lang = _lang,
                SortingField = Constant.Fields.Modified,
                IsSortingAscending = false,
                PageSize = 5
            };
            var items = _mainBannerService.GetAll(query);
            return Ok(items);

        }

        // just for test Imemorycache
        [MemoryCacheFilter(CacheKey = "news", ResultType = typeof(List<NewsViewModel>))]
        public IActionResult get()
        {
            var news = new List<NewsViewModel> { 
            new NewsViewModel{ Id = 1, Title="news1"},
            new NewsViewModel{ Id = 2, Title="news2"}
            };
            return Ok(news);    
        }

    }
}
