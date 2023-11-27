using AIC.CrossCutting.Interfaces.SPInterfaces;
using AIC.CrossCutting.MailService;
using AIC.Data.ViewModels;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.Interfaces
{
    public interface IService<T> where T : IContentItem
    {
       
        ListItem<T> GetAll(Query query);

        ListItem<T> GetAllFromView(Query query, string viewName);
        ListItem<T> GetAllDocuments(Query query);

        ListItem<T> GetFromAlbums(Query query, string listName);
        T GetById(string lang, int Id, string WebUrl = null);
        T GetAlbumDetails(string lang,string albumName, string WebUrl = null);
        T GetItemAttachment(string lang, int Id, string WebUrl = null);
        T GetItemAttachment(string lang, int Id,string fileName, string WebUrl = null);

        ListItem<T> GetAllWithAttachments(Query query);
        T GetItemWithAttachments(string lang, int Id, string WebUrl = null);
        bool UpdateField(string lang, string field, int id, string value, string WebUrl = null);
        bool AccummulateField(string lang, string field, int id, int value, string WebUrl = null);
        bool AccummulateFolder(string webURL, string folderURL, string fieldName, int value);
        string GetFieldValue(string lang, string field, int id, string WebUrl = null);
        T GetByIdAdmin(string lang, int Id, string WebUrl = null);
        public string UploadDocuments(IFormFile file, string libraryName, string subFolder , string uniqueIdentifier);

        public bool DeleteDocument(string filesUrl);
        public int AddContactUsItem(CreateContactUsBaseViewModel model);
        public List<EmailAddress> GetGroupUsers(string groupName);

    }
}
