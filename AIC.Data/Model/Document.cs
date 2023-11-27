using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class Document : Entity<Guid>
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

        public virtual DocumentType DocumenyType { get; set; }
        public virtual ProfileVersion ProfileVersion { get; set; }
    }
}
