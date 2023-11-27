using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class DocumentType : Entity<int>
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        //public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayTitle { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
