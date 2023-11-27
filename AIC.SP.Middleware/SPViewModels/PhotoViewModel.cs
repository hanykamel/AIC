using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware
{
    public class PhotoViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string name { get; set; }
        public string WebUrl { get => ""; }
        public string FileRef { get; set; }
        public string ListName { get => ""; }
    }
}
