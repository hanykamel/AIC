using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class SocialMediaFeedViewModel : IContentItem
    {
        public string Title { get; set; }
        public string AICSocialMediaType { get; set; }
        public string EmbedCode { get; set; }
        public int Id { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.SocialMediaFeeds; }
    }

    public class SocialMediaFeedLst
    {
        public List<IEnumerable<SocialMediaFeedViewModel>> Facebook { get; set; } = new List<IEnumerable<SocialMediaFeedViewModel>>();
        public List<IEnumerable<SocialMediaFeedViewModel>> LinkedIn { get; set; } = new List<IEnumerable<SocialMediaFeedViewModel>>();
        public List<IEnumerable<SocialMediaFeedViewModel>> Youtube { get; set; } = new List<IEnumerable<SocialMediaFeedViewModel>>();

    }
}
