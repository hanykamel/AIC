using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware.Models
{
    public class ListItem<TItem> where TItem : IContentItem
    {
        public string PagingInfo { get; set; }
        public List<TItem> Items { get; set; }
    }
}
