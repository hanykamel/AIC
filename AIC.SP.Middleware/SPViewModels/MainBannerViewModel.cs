using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class MainBannerViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string ReadMoreUrl { get; set; }
        public string VideoUrl { get; set; }

        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.MainBannerList; }
    }
}
