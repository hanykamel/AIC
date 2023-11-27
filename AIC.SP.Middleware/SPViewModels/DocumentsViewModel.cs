using AIC.CrossCutting.Interfaces.SPInterfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware
{
    public class DocumentsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ""; }
        public string RequestCode { get; set; }
        public  IFormFileCollection Files { get; set; }
    }
}
