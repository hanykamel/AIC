using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Helpers;
using AIC.SP.Middleware.Mapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using AIC.SP.Middleware.Models;
using Microsoft.Extensions.Configuration;
using AIC.CrossCutting.Interfaces.SPInterfaces;
using Microsoft.AspNetCore.Http;
using AIC.SP.Middleware.SPViewModels;
using AIC.CrossCutting.MailService;
using AIC.CrossCutting.ExceptionHandling;

namespace AIC.SP.Middleware.Repositories
{
    public class SharePointRepository<T> : ISharePointRepository<T> where T : IContentItem
    {
        public virtual bool IsDocument { get; set; }
        private readonly IConfiguration _config;

        private IMapper mapper;
        public SharePointRepository(IConfiguration config)
        {
            mapper = AutoMapperConfig.Mapper;
            _config = config;
        }

        public ListItem<T> GetAll(Query query)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            string WebUrl = query.WebUrl == null ? contentItem.WebUrl : "/" + query.WebUrl;
            ListItem<T> result = new ListItem<T>();
            SharePointHelper sharePointHelper = new SharePointHelper(query.Lang, WebUrl, contentItem.ListName,_config);
            var items = sharePointHelper.GetItems(query.Filters, query.PagingInfo, query.PageSize, query.SortingField, query.IsSortingAscending);

            if (items != null)
            {
                try
                {
                result.Items = mapper.Map<List<T>>(items);
                result.PagingInfo = items.ListItemCollectionPosition != null ? items.ListItemCollectionPosition.PagingInfo : "";
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return result;
        }

        public ListItem<T> GetAllFromView(Query query, string viewName)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            string WebUrl = query.WebUrl == null ? contentItem.WebUrl : "/" + query.WebUrl;
            ListItem<T> result = new ListItem<T>();
            SharePointHelper sharePointHelper = new SharePointHelper(query.Lang, WebUrl, contentItem.ListName, _config);
            var items = sharePointHelper.GetItemsFromView(query.Filters, query.PagingInfo,viewName, query.PageSize, query.SortingField, query.IsSortingAscending);

            if (items != null)
            {
                try
                {
                    result.Items = mapper.Map<List<T>>(items);
                    result.PagingInfo = items.ListItemCollectionPosition != null ? items.ListItemCollectionPosition.PagingInfo : "";
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return result;
        }
        public T GetItemAttachment(string lang, int Id, string WebUrl = null)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/"+WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            var items = sharePointHelper.GetAttachment(Id);

            if (items != null)
            {
                return mapper.Map<T>(items);
            }
            return default(T);
        }
        public T GetItemAttachment(string lang, int Id,string fileName, string WebUrl = null)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            var items = sharePointHelper.GetAttachment(Id,fileName);

            if (items != null)
            {
                return mapper.Map<T>(items);
            }
            return default(T);
        }
        public ListItem<T> GetAllWithAttachments(Query query)
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            string WebUrl = query.WebUrl == null ? contentItem.WebUrl : "/" + query.WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(query.Lang,WebUrl,contentItem.ListName,_config);
            var items = sharePointHelper.GetItemsWithAttachment(query.Filters, query.PagingInfo, query.PageSize, query.SortingField, query.IsSortingAscending);

            if (items != null)
            {
                result.Items = mapper.Map<List<T>>(items);
                result.PagingInfo = items.ListItemCollectionPosition != null ? items.ListItemCollectionPosition.PagingInfo : "";
            }
            return result;
        }
       

        public T GetById(string lang, int Id, string WebUrl = null)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            var item = sharePointHelper.GetItemById(Id);
            if (item != null)
            {
                return mapper.Map<T>(item);
            }
            return default(T);
        }
        public T GetByIdAdmin(string lang, int Id, string WebUrl = null)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            var item = sharePointHelper.GetItemByIdAdmin(Id);
            if (item != null)
            {
                return mapper.Map<T>(item);
            }
            return default(T);
        }

        public ListItem<T> GetAllDocuments(Query query)
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            string WebUrl = query.WebUrl == null ? contentItem.WebUrl : "/" + query.WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(query.Lang, WebUrl, contentItem.ListName, _config);
            var items = sharePointHelper.GeDocuments(query.Filters, query.PagingInfo, query.PageSize, query.SortingField, query.IsSortingAscending);
            if (items != null)
            {
                result.Items = mapper.Map<List<T>>(items);
                result.PagingInfo = items.ListItemCollectionPosition != null ? items.ListItemCollectionPosition.PagingInfo : "";
            }
            return result;
        }

        public ListItem<T> GetFromAlbums(Query query, string listName)
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            string WebUrl = query.WebUrl == null ? contentItem.WebUrl : "/" + query.WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(query.Lang, WebUrl, listName, _config);
            try
            {
                var items = sharePointHelper.GetFromAlbums(query.Filters, query.PagingInfo, query.PageSize, query.SortingField, query.IsSortingAscending);
                if (items != null)
                {
                    result.Items = mapper.Map<List<T>>(items);
                    result.PagingInfo = items.ListItemCollectionPosition != null ? items.ListItemCollectionPosition.PagingInfo : "";
                }
                else
                {
                    throw new NotFoundException("ItemNotExist");
                }
                return result;
            }
            catch(Exception e)
            {
                throw new NotFoundException("ItemNotExist");
            }
            
        }

        public T GetAlbumDetails(string lang,string albumName,string WebUrl =null)
        {

            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            string listName = contentItem.ListName + "/" + albumName;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, listName, _config);
            try
            {
                var item = sharePointHelper.GetFolder(WebUrl, listName);
                if (item != null)
                {
                    return mapper.Map<T>(item);
                }
                else
                {
                    throw new NotFoundException("ItemNotExist");
                }
            }
            catch(Exception e)
            {
                throw new NotFoundException("ItemNotExist");
            }

            return default(T);
        }

        public T GetItemWithAttachments(string lang, int Id, string WebUrl = null)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            var items = sharePointHelper.GetItemWithAttachment(Id);

            if (items != null)
            {
                return mapper.Map<T>(items);
            }
            return default(T);
        }

        public bool UpdateField(string lang, string field, int id, string value, string WebUrl = null)
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            return sharePointHelper.UpdateFieldValue(id, field,value);
        }
        public bool AccummulateField(string lang, string field, int id, int value, string WebUrl = null)
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            return sharePointHelper.AccummulateFieldValue(id, field, value);
        }
        public bool AccummulateFolder(string lang, string albumName, string fieldName, int value, string WebUrl = null)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            string listName = contentItem.ListName + "/" + albumName;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, listName, _config);
            return sharePointHelper.AccummulateFolder(WebUrl,listName, fieldName, value);
        }

        public string GetFieldValue(string lang, string field, int id, string WebUrl = null)
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper(lang, WebUrl, contentItem.ListName, _config);
            return sharePointHelper.GetFieldValue(id, field);
        }

        public string UploadDocuments(IFormFile file, string libraryName, string subFolder, string uniquIdentifier = null , string WebUrl = null )
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper("", WebUrl, contentItem.ListName, _config);
            return sharePointHelper.UploadDocuments(file, libraryName,subFolder , uniquIdentifier);

        }

        public bool DeleteDocument(string filesUrl, string WebUrl = null)
        {
            ListItem<T> result = new ListItem<T>();
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper("", WebUrl, contentItem.ListName, _config);
            return sharePointHelper.DeleteDocument(filesUrl);

        }

        public int AddContactUsItem(CreateContactUsBaseViewModel model, string WebUrl = null)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            WebUrl = WebUrl == null ? contentItem.WebUrl : "/" + WebUrl;
            SharePointHelper sharePointHelper = new SharePointHelper("", WebUrl, contentItem.ListName, _config);
            return sharePointHelper.AddContactUsItem(model);
        }
        public List<EmailAddress> GetGroupUsers(string groupName)
        {
            IContentItem contentItem = (IContentItem)Activator.CreateInstance(typeof(T));
            SharePointHelper sharePointHelper = new SharePointHelper("", "", contentItem.ListName, _config);
            return sharePointHelper.GetGroupUsers(groupName);
        }

    }
}
