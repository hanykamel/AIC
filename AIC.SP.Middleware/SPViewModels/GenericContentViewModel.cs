using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class GenericContentViewModel : IContentItem
    {
        public string Title { get; set; }
        public string AICDesc { get; set; }
        public string AICImage { get; set; }
        public string GenericContentType { get; set; }
        public int Id { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.GenericContent; }
    }
}
