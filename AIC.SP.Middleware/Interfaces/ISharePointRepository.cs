using AIC.CrossCutting.Interfaces.SPInterfaces;
using AIC.SP.Middleware.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware.Interfaces
{
    public interface ISharePointRepository<T> where T : IContentItem
    {
        ListItem<T> GetAll(Query query);

        ListItem<T> GetAllFromView(Query query, string viewName);
        ListItem<T> GetAllDocuments(Query query);

        ListItem<T> GetFromAlbums(Query query, string listName);
        T GetById(string lang, int Id,string WebUrl = null);
        T GetAlbumDetails(string lang,string albumName, string WebUrl = null);
        T GetItemAttachment(string lang, int Id, string WebUrl = null);
        T GetItemAttachment(string lang, int Id,string fileName, string WebUrl = null);
        ListItem<T> GetAllWithAttachments(Query query);
        T GetItemWithAttachments(string lang, int Id, string WebUrl = null);
        bool UpdateField(string lang, string field, int id, string value, string WebUrl = null);
        string GetFieldValue(string lang, string field, int id, string WebUrl = null);

    }
}
