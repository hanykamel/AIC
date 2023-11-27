using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware
{
    public class PhotoAlbumViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AlbumCoverImage { get; set; }
        public DateTime? AlbumDate { get; set; }
        public string Brief { get; set; }
        public string FileRef { get; set; }
        public string WebUrl { get => ""; }
        public string ListName { get => ListsNames.PhotoGallery; }
    }
}
