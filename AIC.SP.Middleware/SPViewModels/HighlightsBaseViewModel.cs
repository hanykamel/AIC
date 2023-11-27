using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.SPViewModels
{
    public class HighlightsBaseViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string ThumbnailImage { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime HighlightDate { get; set; }
        public string WebUrl { get => ""; }

        public string ListName { get; set; }
    }
}
