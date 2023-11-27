using AIC.Data.ViewModels.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Data.ViewModels
{
    public class DocumentDBViewModel
    {
        public Guid ProfileVersionId { get; set; }
        public string DisplayTitle { get; set; }
        public string Extention { get; set; }
        public decimal Size { get; set; }
        public string ContentType { get; set; }
        public string DocumentUrl { get; set; }
        public int? SharepointId { get; set; }
        public string FolderUrl { get; set; }
        public int DocumenyTypeId { get; set; }
        public DocumentTypeViewModel DocumenyType { get; set; }
    }
}
