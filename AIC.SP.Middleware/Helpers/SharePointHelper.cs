using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Configuration;
using Microsoft.SharePoint.Client;
using Microsoft.Extensions.Configuration;
using AIC.SP.Middleware.Models;
using AIC.CrossCutting.ExceptionHandling;
using System.IO;
using Microsoft.AspNetCore.Http;
using Asset.SharePoint.Middleware.Helpers;
using AIC.SP.Middleware.SPViewModels;
using System.Linq;
using AIC.CrossCutting.MailService;

namespace AIC.SP.Middleware.Helpers
{
    public class SharePointHelper
    {
        public string WebUrl { get; set; }
        public string SiteUrl { get; private set; }
        public string ListName { get; set; }
        public string WebFullUrl { get; private set; }
        public string Language { get; set; }
        private NetworkCredential Credential { get; set; }
        private readonly IConfiguration config;
        public SharePointHelper(string language, string webUrl, string listName, IConfiguration config)
        {
            Language = language;
            WebUrl = webUrl;
            ListName = listName;
            this.config = config;
            var SharepointSection = this.config.GetSection("Sharepoint");

            SiteUrl = SharepointSection.GetSection("SiteUrl").Value;
            if (!string.IsNullOrEmpty(language))
                WebFullUrl = SiteUrl + Language + WebUrl;
            else
                WebFullUrl = SiteUrl.Remove(SiteUrl.Length - 1) + webUrl;
            Credential = new NetworkCredential(SharepointSection.GetSection("ViewerUserName").Value, SharepointSection.GetSection("Password").Value);
        }

        #region Methods

        public ListItemCollection GetItemsFromView(List<Filter> filters, string pagingInfo, string viewName, int pageSize = 10, string sortingField = "Title", bool isSortingAscending = true)
        {
            ListItemCollection items = null;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = Credential;
                List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                context.Load(list);
                context.ExecuteQueryAsync();
                View view = list.Views.GetByTitle(viewName);
                context.Load(view);
                context.ExecuteQueryAsync();
                CamlQuery camlQuery = new CamlQuery
                {
                    ViewXml = $@"<View DisplayName=" + viewName +
                            @"Name=" + viewName +
                                @" DefaultView='TRUE'> 
                                <Query> {GetQueryXml(filters)}
                                    <OrderBy>
                                        <FieldRef Name='{sortingField}' Ascending='{isSortingAscending}' />
                                    </OrderBy>
                                </Query>
                                <RowLimit Paged='TRUE' >{pageSize}</RowLimit>
                             </View>"
                };
                if (pagingInfo != null)

                    camlQuery.ListItemCollectionPosition = new ListItemCollectionPosition()
                    {
                        PagingInfo = pagingInfo
                    };


                items = list.GetItems(camlQuery);
                context.Load(items);
                context.ExecuteQueryAsync().Wait();


            }
            return items;
        }

        public ListItemCollection GetItems(List<Filter> filters, string pagingInfo, int pageSize = 10, string sortingField = "Title", bool isSortingAscending = true)
        {
            ListItemCollection items = null;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = Credential;
                List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                //Group group = context.Web.SiteGroups.GetByName("");
                //UserCollection users = group.Users;
                CamlQuery camlQuery = new CamlQuery
                {
                    ViewXml = $@"<View> 
                                <Query> 
                                    {GetQueryXml(filters)}
                                    <OrderBy>
                                        <FieldRef Name='{sortingField}' Ascending='{isSortingAscending}' />
                                    </OrderBy>
                                </Query>
                                <RowLimit Paged='TRUE' >{pageSize}</RowLimit>
                             </View>"
                };
                if (pagingInfo != null)
                    camlQuery.ListItemCollectionPosition = new ListItemCollectionPosition()
                    {
                        PagingInfo = pagingInfo
                    };
                items = list.GetItems(camlQuery);
                context.Load(items);
                context.ExecuteQueryAsync().Wait();


            }
            return items;
        }

        public ListItemCollection GeDocuments(List<Filter> filters, string pagingInfo, int pageSize = 10, string sortingField = "Title", bool isSortingAscending = true)
        {
            ListItemCollection items = null;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = Credential;

                //   context.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                List list = context.Web.GetList(WebFullUrl + $"/{ListName}");
                CamlQuery camlQuery = new CamlQuery
                {
                    ViewXml = $@"<View> 
                                <Query> 
                                    {GetQueryXml(filters)}
                                    <OrderBy>
                                        <FieldRef Name='{sortingField}' Ascending='{isSortingAscending}' />
                                    </OrderBy>
                                </Query>
                                <RowLimit Paged='TRUE' >{pageSize}</RowLimit>
                             </View>"
                };
                if (pagingInfo != null)
                    camlQuery.ListItemCollectionPosition = new ListItemCollectionPosition()
                    {
                        PagingInfo = pagingInfo
                    };
                items = list.GetItems(camlQuery);
                context.Load(items);
                context.ExecuteQueryAsync().Wait();
                return items;
            }

        }

        public ListItemCollection GetFromAlbums(List<Filter> filters, string pagingInfo, int pageSize = 10, string sortingField = "Title", bool isSortingAscending = true)
        {
            ListItemCollection items = null;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = Credential;

                //context.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                List list = context.Web.GetList(WebFullUrl + $"/{ListName}");
                CamlQuery camlQuery = new CamlQuery
                {
                    ViewXml = $@"<View Scope='FilesOnly'> 
                                <Query> 
                                    {GetQueryXml(filters)}
                                    <OrderBy>
                                        <FieldRef Name='{sortingField}' Ascending='{isSortingAscending}' />
                                    </OrderBy>
                                </Query>
                                <RowLimit Paged='TRUE' >{pageSize}</RowLimit>
                             </View>"
                };

                camlQuery.FolderServerRelativeUrl = "/" + Language + "/" + ListName;
                if (pagingInfo != null)
                    camlQuery.ListItemCollectionPosition = new ListItemCollectionPosition()
                    {
                        PagingInfo = pagingInfo
                    };
                items = list.GetItems(camlQuery);
                context.Load(items);
                context.ExecuteQueryAsync().Wait();
                return items;
            }

        }

        public ListItem GetFolder(string webURL, string folderURL)
        {

            using (ClientContext clientContext = new ClientContext(WebFullUrl))
            {
                clientContext.Credentials = Credential;
                // clientContext.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                var folder = clientContext.Web.GetFolderByServerRelativeUrl(folderURL).ListItemAllFields;
                clientContext.Load(folder);
                clientContext.ExecuteQueryAsync().Wait();
                return folder;
            }
        }

        public ListItem GetItemById(int id)
        {
            if (id <= 0)
                throw new NotFoundException("Not Found");
            try
            {
                using (ClientContext context = new ClientContext(WebFullUrl))
                {
                    context.Credentials = Credential;
                    List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                    ListItem item = list.GetItemById(id);
                    context.Load(item);
                    context.ExecuteQueryAsync().Wait();
                    if (item != null)
                        return item;
                    else
                        throw new NotFoundException("Not Found");
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException("ItemNotExist");
            }
        }
        public ListItem GetItemByIdAdmin(int id)
        {
            if (id <= 0)
                throw new NotFoundException("Not Found");
            try
            {
                var SharepointSection = this.config.GetSection("Sharepoint");
                NetworkCredential administratorCredntioal = new NetworkCredential(SharepointSection.GetSection
                 ("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);
                using (ClientContext context = new ClientContext(WebFullUrl))
                {
                    context.Credentials = administratorCredntioal;
                    List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                    ListItem item = list.GetItemById(id);
                    context.Load(item);
                    context.ExecuteQueryAsync().Wait();
                    if (item != null)
                        return item;
                    else
                        throw new NotFoundException("Not Found");
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public ListItemCollection GetItemsWithAttachment(List<Filter> filters, string pagingInfo, int pageSize = 10, string sortingField = "Title", bool isSortingAscending = true)
        {
            ListItemCollection items = null;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = Credential;
                List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                CamlQuery camlQuery = new CamlQuery
                {
                    ViewXml = $@"<View> 
                                <Query> 
                                    {GetQueryXml(filters)}
                                    <OrderBy>
                                        <FieldRef Name='{sortingField}' Ascending='{isSortingAscending}' />
                                    </OrderBy>
                                </Query>
                                <RowLimit Paged='TRUE' >{pageSize}</RowLimit>
                             </View>"
                };
                if (pagingInfo != null)
                    camlQuery.ListItemCollectionPosition = new ListItemCollectionPosition()
                    {
                        PagingInfo = pagingInfo
                    };
                items = list.GetItems(camlQuery);
                context.Load(items);
                context.ExecuteQueryAsync().Wait();
                foreach (ListItem listItem in items)
                {
                    AttachmentCollection oAttachments = listItem.AttachmentFiles;
                    context.Load(oAttachments);
                    context.ExecuteQueryAsync().Wait();
                    if (oAttachments.Count > 0)
                    {
                        List<string> attachmentNames = new List<string>();
                        if (oAttachments.Count > 0)
                        {
                            foreach (var attachmentItem in oAttachments)
                            {
                                attachmentNames.Add(attachmentItem.FileName);
                            }
                            listItem.FieldValues["File_x0020_Type"] = String.Join(", ", attachmentNames.ToArray());
                        }
                        //Attachment oAttachment = oAttachments[0];
                        //listItem.FieldValues["File_x0020_Type"] = oAttachment.FileName;
                    }

                }

            }
            return items;
        }

        public ListItem GetAttachment(int id)
        {
            ListItem item = null;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = Credential;
                List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                item = list.GetItemById(id);
                context.Load(item);
                context.ExecuteQueryAsync().Wait();
                AttachmentCollection oAttachments = item.AttachmentFiles;
                context.Load(oAttachments);
                context.ExecuteQueryAsync().Wait();
                if (oAttachments.Count > 0)
                {
                    var file = context.Web.GetFileByServerRelativeUrl(oAttachments[0].ServerRelativeUrl);
                    context.Load(file);
                    context.ExecuteQueryAsync().Wait();
                    ClientResult<System.IO.Stream> data = file.OpenBinaryStream();
                    context.Load(file);
                    context.ExecuteQueryAsync().Wait();
                    using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                    {
                        if (data != null)
                        {
                            data.Value.CopyTo(mStream);
                            byte[] fileBytes = mStream.ToArray();
                            item.FieldValues["File_x0020_Type"] = fileBytes;
                        }
                    }
                }
            }
            return item;
        }

        public ListItem GetAttachment(int id, string filename)
        {
            ListItem item = null;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = Credential;
                string serverUrl = WebFullUrl + $"/Lists/{ListName}";
                string serverRelUrl = "/" + Language + $"/Lists/{ListName}";
                List list = context.Web.GetList(serverUrl);
                item = list.GetItemById(id);
                context.Load(item);
                context.ExecuteQueryAsync().Wait();
                AttachmentCollection oAttachments = item.AttachmentFiles;
                context.Load(oAttachments);
                context.ExecuteQueryAsync().Wait();
                if (oAttachments.Count > 0)
                {
                    var file = context.Web.GetFileByServerRelativeUrl(serverRelUrl + "/Attachments/" + id + "/" + filename);
                    context.Load(file);
                    context.ExecuteQueryAsync().Wait();
                    ClientResult<System.IO.Stream> data = file.OpenBinaryStream();
                    context.Load(file);
                    context.ExecuteQueryAsync().Wait();
                    using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                    {
                        if (data != null)
                        {
                            data.Value.CopyTo(mStream);
                            byte[] imageArray = mStream.ToArray();
                            item.FieldValues["File_x0020_Type"] = imageArray;
                        }
                    }
                }
            }
            return item;
        }

        public ListItem GetItemWithAttachment(int id)
        {
            ListItem item = null;
            try
            {
                using (ClientContext context = new ClientContext(WebFullUrl))
                {
                    context.Credentials = Credential;
                    List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                    item = list.GetItemById(id);
                    context.Load(item);
                    context.ExecuteQueryAsync().Wait();
                    AttachmentCollection oAttachments = item.AttachmentFiles;
                    context.Load(oAttachments);
                    context.ExecuteQueryAsync().Wait();
                    List<string> attachmentNames = new List<string>();
                    if (oAttachments.Count > 0)
                    {
                        foreach (var attachmentItem in oAttachments)
                        {
                            attachmentNames.Add(attachmentItem.FileName);
                        }
                        item.FieldValues["File_x0020_Type"] = String.Join(", ", attachmentNames.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return item;

        }

        public bool UpdateFieldValue(int id, string fieldName, string value)
        {
            if (id <= 0)
                throw new NotFoundException("Not Found");
            try
            {
                ListItem item = null;
                var SharepointSection = this.config.GetSection("Sharepoint");
                NetworkCredential administratorCredntioal = new NetworkCredential(SharepointSection.GetSection
                    ("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);
                using (ClientContext context = new ClientContext(WebFullUrl))
                {
                    context.Credentials = administratorCredntioal;
                    string serverUrl = WebFullUrl + $"/Lists/{ListName}";
                    string serverRelUrl = "/" + Language + $"/Lists/{ListName}";
                    List list = context.Web.GetList(serverUrl);
                    item = list.GetItemById(id);
                    item[fieldName] = value;
                    item.SystemUpdate();
                    context.ExecuteQueryAsync().Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new SPException(ex.Message);
            }
        }


        public bool AccummulateFolder(string webURL, string folderURL, string fieldName, int value)
        {
            try
            {
                var SharepointSection = this.config.GetSection("Sharepoint");
                NetworkCredential administratorCredntioal = new NetworkCredential(SharepointSection.GetSection
                    ("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);
                using (ClientContext clientContext = new ClientContext(WebFullUrl))
                {
                    clientContext.Credentials = administratorCredntioal;
                    var folder = clientContext.Web.GetFolderByServerRelativeUrl(folderURL).ListItemAllFields;
                    clientContext.Load(folder);
                    clientContext.ExecuteQueryAsync().Wait();
                    folder[fieldName] = Convert.ToInt32(folder[fieldName]) + value;
                    folder.SystemUpdate();
                    clientContext.ExecuteQueryAsync().Wait();
                }
                return true;
            }
            catch (Exception)
            {
                //throw new SPException(ex.Message);
                return true;
            }
        }
        public bool AccummulateFieldValue(int id, string fieldName, int value)
        {
            try
            {
                ListItem item = null;
                var SharepointSection = this.config.GetSection("Sharepoint");
                NetworkCredential administratorCredntioal = new NetworkCredential(SharepointSection.GetSection("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);
                using (ClientContext context = new ClientContext(WebFullUrl))
                {
                    context.Credentials = administratorCredntioal;
                    string serverUrl = WebFullUrl + $"/Lists/{ListName}";
                    string serverRelUrl = "/" + Language + $"/Lists/{ListName}";
                    List list = context.Web.GetList(serverUrl);
                    item = list.GetItemById(id);
                    context.Load(item);
                    context.ExecuteQueryAsync().Wait();

                    item[fieldName] = Convert.ToInt32(item[fieldName]) + value;
                    item.SystemUpdate();
                    context.ExecuteQueryAsync().Wait();
                }
                return true;
            }
            catch (Exception)
            {
                //throw new SPException(ex.Message);
                return true;
            }
        }


        public string UploadDocuments(IFormFile file, string libraryName, string subFolder, string uniquIdentifier)
        {
            try
            {
                string filesUrls = "";
                bool isRoot = true;
                string rootFolderName = "";
                List<string> uploadedFilesUrls = new List<string>();
                var SharepointSection = this.config.GetSection("Sharepoint");
                NetworkCredential administratorCredential = new NetworkCredential(SharepointSection.GetSection
                    ("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);
                //if (files.Count < 1)
                //{
                //    throw new NotFoundException("EmptyFiles");
                //}
                if (file is null)
                    throw new NotFoundException("EmptyFiles");

                var str = file.FileName.Split('.', 6);
                var extension = str[str.Length - 1];
                string hostURL = SharepointFieldHelpers.getHostURL();

                if (libraryName.Contains('/'))
                {
                    isRoot = false;
                    rootFolderName = libraryName.Split('/')[1];
                    libraryName = libraryName.Split('/')[0];
                }

                //foreach (IFormFile file in files)
                //{
                using (ClientContext context = new ClientContext(WebFullUrl))
                {

                    context.Credentials = administratorCredential;
                    string libraryUrl = WebFullUrl + $"/{libraryName}";
                    List documentLibrary = context.Web.GetList(libraryUrl);

                    FileCreationInformation newFile = new FileCreationInformation();
                    newFile.ContentStream = file.OpenReadStream();
                    newFile.Url = (uniquIdentifier is not null ? uniquIdentifier : string.Empty) + file.FileName + (uniquIdentifier is not null ? uniquIdentifier : string.Empty) +"."+ extension;
                    newFile.Overwrite = true;

                    Folder rootFolder;
                    if (isRoot)
                    {
                        rootFolder = documentLibrary.RootFolder;
                    }
                    else
                    {
                        rootFolder = documentLibrary.RootFolder.Folders.Add(rootFolderName);
                    }
                    //Folder requestFolder = rootFolder.Folders.Add(subFolder);
                    Folder requestFolder = rootFolder;
                    requestFolder.Update();
                    Microsoft.SharePoint.Client.File uploadFile = requestFolder.Files.Add(newFile);

                    context.Load(documentLibrary);
                    context.Load(uploadFile);
                    context.ExecuteQueryAsync().Wait();

                    //string uploadedFileUrl = hostURL + libraryName+'/'+ (isRoot?subFolder: rootFolderName + '/'+ subFolder) + '/' + file.FileName;
                    string uploadedFileUrl = hostURL + libraryName + '/' + (uniquIdentifier is not null ? uniquIdentifier : string.Empty) + file.FileName + (uniquIdentifier is not null ? uniquIdentifier : string.Empty) +"."+ extension;
                    //uploadedFilesUrls.Add(uploadedFileUrl);
                    filesUrls = uploadedFileUrl;
                }
                //}
                //filesUrls = string.Join(",", uploadedFilesUrls);
                return filesUrls;
            }
            catch (Exception ex)
            {
                throw new SPException(ex.Message);
            }
        }

        public bool DeleteDocument(string filesUrl)
        {
            try
            {
                var SharepointSection = this.config.GetSection("Sharepoint");
                NetworkCredential administratorCredential = new NetworkCredential(SharepointSection.GetSection
                    ("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);

                string[] filesUrlsArr = filesUrl.Split(',');
                using (ClientContext context = new ClientContext(WebFullUrl))
                {
                    context.Credentials = administratorCredential;
                    string hostURL = SharepointFieldHelpers.getHostURL();

                    foreach (string singlefileUrl in filesUrlsArr)
                    {
                        if (!String.IsNullOrEmpty(singlefileUrl.Trim()))
                        {
                            string fileUrl = singlefileUrl.Trim();
                            fileUrl = fileUrl.Contains(hostURL) ? fileUrl.Replace(hostURL, SiteUrl) : fileUrl;

                            Microsoft.SharePoint.Client.File file = context.Web.GetFileByUrl(fileUrl);
                            context.Load(file);
                            file.DeleteObject();
                            context.ExecuteQueryAsync().Wait();
                        }
                    }


                }
                return true;
            }
            catch (Exception)
            {
                throw new SPException("FileNotFound");
            }
        }


        #endregion
        public string GetFieldValue(int id, string fieldName)

        {
            if (id <= 0)
                throw new NotFoundException("Not Found");
            try
            {

                String attendees = "";
                ListItem item = null;
                var SharepointSection = this.config.GetSection("Sharepoint");
                NetworkCredential administratorCredntioal = new NetworkCredential(SharepointSection.GetSection("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);
                using (ClientContext context = new ClientContext(WebFullUrl))
                {
                    context.Credentials = administratorCredntioal;
                    string serverUrl = WebFullUrl + $"/Lists/{ListName}";
                    string serverRelUrl = "/" + Language + $"/Lists/{ListName}";
                    List list = context.Web.GetList(serverUrl);
                    item = list.GetItemById(id);
                    context.Load(item);
                    context.ExecuteQueryAsync().Wait();
                    if (item[fieldName] != null)
                        attendees = item[fieldName].ToString();
                }
                return attendees;
            }
            catch (Exception ex)
            {
                throw new SPException(ex.Message);
            }
        }

        public List<MenuItem> GetMenuItems(List<MainMenuViewModel> items)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            var result = items.GroupBy(i => i.Parent, u => u.Id, (p, u) => new { Parent = p, Children = u.ToList() });
            var headerItems = result.Where(i => string.IsNullOrEmpty(i.Parent));
            //menu items
            foreach (var item in headerItems)
            {
                foreach (var child in item.Children)
                {
                    menuItems.Add(MapMenuItem(items.FirstOrDefault(i => i.Id == child)));
                }
            }
            //sub items level 1
            foreach (var item in menuItems)
            {
                var subItem = result.Where(i => i.Parent == item.TitleEn).FirstOrDefault();
                if (subItem != null)
                {
                    item.Children = new List<MenuItem>();
                    foreach (var child in subItem.Children)
                    {
                        item.Children.Add(MapMenuItem(items.FirstOrDefault(i => i.Id == child)));
                    }
                }
            }
            return menuItems;
        }
        #region Helpers
        private static string GetQueryXml(List<Filter> filters)
        {
            string viewXml = "";
            if (filters != null)
            {
                viewXml += "<Where>";
                for (int i = 0; i < filters.Count - 1; i++)
                {
                    viewXml += "<And>";
                }
                // or
                for (int i = 0; i < filters.Count; i++)
                {
                    if (filters[i].FieldValueType.ToLower() == "lookupid"
                        || filters[i].FieldValueType.ToLower() == "lookup"
                        || filters[i].FieldValueType.ToLower() == "lookupmulti")
                    {
                        viewXml += @"<" + filters[i].Operator + @"><FieldRef Name='" + filters[i].Field + @"' LookupId='TRUE' /><Value Type='" + filters[i].FieldValueType + @"' >" + filters[i].Value + @"</Value></" + filters[i].Operator + @">";
                    }
                    else
                    {
                        //old
                        string[] ORFields = filters[i].Field.Split(',');
                        for (int k = 0; k < ORFields.Length - 1; k++)
                        {
                            viewXml += "<Or>";
                        }
                        for (int k = 0; k < ORFields.Length; k++)
                        {

                            viewXml += @"<" + filters[i].Operator + @"><FieldRef Name='" + ORFields[k] + @"' />";
                            if (filters[i].Operator != "IsNotNull" && filters[i].Operator != "IsNull")
                            {
                                viewXml += @"<Value Type='" + filters[i].FieldValueType + @"'>" + filters[i].Value + @"</Value></" + filters[i].Operator + @">";
                            }
                            else
                            {
                                viewXml += @"</" + filters[i].Operator + @">";
                            }
                            if (k >= 1 && ORFields.Length > 2)
                            {
                                viewXml += " </Or>";
                            }

                        }
                        if (ORFields.Length <= 2)
                        {
                            for (int k = 0; k < ORFields.Length - 1; k++)
                            {
                                viewXml += "</Or>";
                            }
                        }
                    }
                    if (i >= 1 && filters.Count > 2)
                    {
                        viewXml += "</And>";
                    }
                }
                if (filters.Count <= 2)
                {
                    for (int i = 0; i < filters.Count - 1; i++)
                    {
                        viewXml += "</And>";
                    }
                }
                viewXml += "</Where>";
            }
            return viewXml;
        }


        public int AddContactUsItem(CreateContactUsBaseViewModel model)
        {
            int id = -1;
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                var SharepointSection = this.config.GetSection("Sharepoint");
                var SharepointSec = config.GetSection("Sharepoint");
                context.Credentials = new NetworkCredential(SharepointSec.GetSection("EditorUserName").Value, SharepointSection.GetSection("EditorPassword").Value);
                List list = context.Web.GetList(WebFullUrl + $"/Lists/{ListName}");
                ListItemCreationInformation oListItemCreationInformation = new ListItemCreationInformation();
                ListItem item = list.AddItem(oListItemCreationInformation);
                // add contact us sharepoint columns here
                FieldLookupValue countryLookup = new FieldLookupValue();
                FieldLookupValue cityLookup = new FieldLookupValue();
                item["Title"] = model.FullName;
                item["AICEmail"] = model.Email;
                item["AICPhoneNumber"] = model.PhoneNumber;
                item["AICDate"] = DateTime.Now.ToString("MM-dd-yyyy");
                item["AICPhoneNumber"] = model.PhoneNumber;
                item["ReferenceNumber"] = model.RequestNumber;
                countryLookup.LookupId = model.CountryId;
                item["Country"] = countryLookup;
                //not mandatory
                if (model.CityId != 0)
                {
                    cityLookup.LookupId = model.CityId;
                    item["AICCity"] = cityLookup;
                }
                item["AICWorkEducationalOrganization"] = model.WorkEducationalOrganization;
                item["AICMessage"] = model.Message;
                item.Update();
                context.ExecuteQueryAsync().Wait();
                context.Load(item);
                id = item.Id;

            }
            return id;

        }

        public List<EmailAddress> GetGroupUsers(string groupName)
        {
            List<EmailAddress> emails;
            var SharepointSection = this.config.GetSection("Sharepoint");
            NetworkCredential administratorCredential = new NetworkCredential(SharepointSection.GetSection
                ("AdministratorUserName").Value, SharepointSection.GetSection("AdministratorPassword").Value);
            using (ClientContext context = new ClientContext(WebFullUrl))
            {
                context.Credentials = administratorCredential;
                Group group = context.Web.SiteGroups.GetByName(groupName);
                var users = group.Users;
                context.Load(users);
                context.ExecuteQueryAsync().Wait();
                emails = group.Users.Where(u => !string.IsNullOrEmpty(u.Email)).Select(u => new EmailAddress { Address = u.Email, Name = u.Title }).ToList();
            }
            return emails;
        }
        private MenuItem MapMenuItem(MainMenuViewModel mainMenuViewModel)
        {
            var menuItem = new MenuItem();
            menuItem.Id = mainMenuViewModel.Id;
            menuItem.TitleEn = mainMenuViewModel.Title;
            menuItem.TitleAr = mainMenuViewModel.TitleAr;
            menuItem.Parent = mainMenuViewModel.Parent;
            menuItem.HasChildren = mainMenuViewModel.HasChildren;
            menuItem.Url = mainMenuViewModel.Url;
            menuItem.OtherUrl = mainMenuViewModel.OtherUrl;
            menuItem.Order = mainMenuViewModel.Order;
            menuItem.Hidden = mainMenuViewModel.Hidden;
            return menuItem;
        }

        #endregion
    }
}
