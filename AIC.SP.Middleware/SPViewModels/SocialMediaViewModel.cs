using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class SocialMediaViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Logo { get; set; }

        public string HomeLogo { get; set; }

        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.SocialMediaList; }
    }
}
