using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.Interfaces.SPInterfaces
{
    public interface IListItem<TItem> where TItem : IContentItem
    {
        string PagingInfo { get; set; }
        List<TItem> Items { get; set; }
    }
}
