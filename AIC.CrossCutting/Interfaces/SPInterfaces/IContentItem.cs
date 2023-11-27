using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.CrossCutting.Interfaces.SPInterfaces
{
    public interface IContentItem
    {
        int Id { get; set; }
        string Title { get; set; }
        string WebUrl { get; }
        string ListName { get; }
    }
}
