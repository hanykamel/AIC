using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware
{
    public class VideoViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get ; set ; }
        public string EmbedCode { get; set; }
        public string URL { get; set; }
        public string Src { get; set; }
        public string ThumbURL { get; set; }
        public DateTime VideoDate { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ""; }
    }
}
