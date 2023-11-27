using AIC.CrossCutting.Interfaces.SPInterfaces;
using AIC.CrossCutting.MailService;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.Repositories;
using AIC.SP.Middleware.SPViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AIC.SP.Middleware.Services
{
    public class Service<T> : IService<T> where T : IContentItem
    {
        readonly SharePointRepository<T> _sharePointRepository;
        public static IConfiguration configurations;

        public Service(IConfiguration config)
        {
            _sharePointRepository = new SharePointRepository<T>(config);
            configurations = config;
        }
        public ListItem<T> GetAll(Query query)
        {
            return _sharePointRepository.GetAll(query);
        }

     
        public T GetById(string lang, int Id, string WebUrl = null)
        {
            return _sharePointRepository.GetById(lang, Id, WebUrl);
        }

        public ListItem<T> GetAllDocuments(Query query)
        {
            return _sharePointRepository.GetAllDocuments(query);
        }

        public ListItem<T> GetFromAlbums(Query query, string listName)
        {
            return _sharePointRepository.GetFromAlbums(query, listName);
        }

        public T GetAlbumDetails(string lang,string albumName, string WebUrl)
        {
            return _sharePointRepository.GetAlbumDetails(lang,albumName, WebUrl);
        }

        public T GetItemAttachment(string lang, int Id, string WebUrl = null)
        {
            return _sharePointRepository.GetItemAttachment(lang, Id, WebUrl);
        }
        public T GetItemAttachment(string lang, int Id,string fileName, string WebUrl = null)
        {
            return _sharePointRepository.GetItemAttachment(lang, Id,fileName, WebUrl);
        }

        public ListItem<T> GetAllWithAttachments(Query query)
        {
            return _sharePointRepository.GetAllWithAttachments(query);
        }
        public T GetItemWithAttachments(string lang, int Id, string WebUrl = null)
        {
            return _sharePointRepository.GetItemWithAttachments(lang,Id,WebUrl);
        }
        public bool UpdateField(string lang, string field, int id, string value, string WebUrl = null)
        {
            return _sharePointRepository.UpdateField(lang, field, id, value, WebUrl);
        }

        public bool AccummulateField(string lang, string field, int id, int value, string WebUrl = null)
        {
            return _sharePointRepository.AccummulateField(lang, field, id, value, WebUrl);
        }
        public bool AccummulateFolder(string webURL, string folderURL, string fieldName, int value)
        {
            return _sharePointRepository.AccummulateFolder(webURL, folderURL, fieldName, value);
        }
        public string GetFieldValue(string lang, string field, int id, string WebUrl = null)
        {
            return _sharePointRepository.GetFieldValue(lang, field, id, WebUrl);
        }

        public T GetByIdAdmin(string lang, int Id, string WebUrl = null)
        {
            return _sharePointRepository.GetByIdAdmin(lang, Id, WebUrl);
        }

        public string UploadDocuments(IFormFile file, string libraryName, string subFolder , string uniqueIdentifier)
        {
            string documentUrl = _sharePointRepository.UploadDocuments(file, libraryName, subFolder, uniqueIdentifier);
            var SharepointSection = configurations.GetSection("Sharepoint");
            string publishedURL = SharepointSection.GetSection("PublishedURL").Value;
            string hostURL = SharepointSection.GetSection("HostURL").Value;
            documentUrl = documentUrl != null ? documentUrl.Replace(hostURL, publishedURL + "/").ToString() : "";
            return documentUrl;
        }
        public bool DeleteDocument(string filesUrl)
        {
            return _sharePointRepository.DeleteDocument(filesUrl);
        }

        public ListItem<T> GetAllFromView(Query query, string viewName)
        {
            return _sharePointRepository.GetAllFromView(query, viewName);
        }

        public int AddContactUsItem(CreateContactUsBaseViewModel model)
        {
            return _sharePointRepository.AddContactUsItem(model);
        }

        public List<EmailAddress> GetGroupUsers(string groupName)
        {
            return _sharePointRepository.GetGroupUsers(groupName);
        }
    }
}
