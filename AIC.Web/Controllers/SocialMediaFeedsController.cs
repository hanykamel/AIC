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
    public class SocialMediaFeedsController : BaseController
    {
        private readonly IService<SocialMediaFeedViewModel> _socialMediaFeedsService;

        public SocialMediaFeedsController(IService<SocialMediaFeedViewModel> socialMediaFeedsService)
        {
            _socialMediaFeedsService = socialMediaFeedsService;
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> List([FromBody] Query query)
        {

            SocialMediaFeedLst itemsLst = new SocialMediaFeedLst();
            var items = _socialMediaFeedsService.GetAll(query);
            itemsLst.Facebook.Add(items.Items.FindAll(x => x.AICSocialMediaType.ToString() == (_lang == "en"? SocialMediaType.Facebook : SocialMediaType.FacebookAr)).Take(6));
            itemsLst.LinkedIn.Add(items.Items.FindAll(x => x.AICSocialMediaType.ToString() == (_lang == "en" ? SocialMediaType.LinkedIn : SocialMediaType.LinkedInAr)).Take(6));
            itemsLst.Youtube.Add(items.Items.FindAll(x => x.AICSocialMediaType.ToString() == (_lang == "en" ? SocialMediaType.YouTube : SocialMediaType.YouTubeAr)).Take(6));
            return Ok(itemsLst);
        }
    }
}
