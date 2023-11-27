using AIC.CrossCutting.Constant;
using AIC.CrossCutting.MailService;
using AIC.Data.Enums;
using AIC.Service.Entities.CommonActions.ContactUs;
using AIC.Service.Interfaces;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Service.Implementation
{
    public class ContactUsBusiness : IContactUsBusiness
    {
        private readonly IService<ContactUsFormsCareersViewModel> _contactUsFormsCareersService;
        private readonly IService<ContactUsFormsComplaintsViewModel> _ContactUsFormsComplaintsService;
        private readonly IService<ContactUsFormsFeedbacksViewModel> _contactUsFormsFeedbacksService;
        private readonly IService<ContactUsFormsHPCViewModel> _contactUsFormsHPCService;
        private readonly IService<ContactUsFormsHumanResourcesViewModel> _contactUsFormsHumanResourcesService;
        private readonly IService<ContactUsFormsInquiryViewModel> _contactUsFormsInquiryService;
        private readonly IService<ContactUsRDProjectsViewModel> _contactUsFormsRDProjectsService;
        private readonly IService<ContactUsFormsSuggestionsViewModel> _contactUsFormsSuggestionsService;
        private ISendEmail _sendEmailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        ILogger<ContactUsBusiness> _logger;
        public ContactUsBusiness(IService<ContactUsFormsCareersViewModel> contactUsFormsCareersService, ISendEmail sendEmailService,
            IService<ContactUsFormsComplaintsViewModel> ContactUsFormsComplaintsService,
            IService<ContactUsFormsFeedbacksViewModel> contactUsFormsFeedbacksService,
            IService<ContactUsFormsHPCViewModel> contactUsFormsHPCService,
            IService<ContactUsFormsHumanResourcesViewModel> contactUsFormsHumanResourcesService,
            IService<ContactUsFormsInquiryViewModel> contactUsFormsInquiryService,
            IService<ContactUsRDProjectsViewModel> contactUsFormsRDProjectsService,
            IService<ContactUsFormsSuggestionsViewModel> contactUsFormsSuggestionsService, 
            IMapper mapper, IConfiguration config, ILogger<ContactUsBusiness> logger)
        {
            _contactUsFormsCareersService = contactUsFormsCareersService;
            _sendEmailService = sendEmailService;
            _ContactUsFormsComplaintsService = ContactUsFormsComplaintsService;
            _contactUsFormsFeedbacksService = contactUsFormsFeedbacksService;
            _contactUsFormsHPCService = contactUsFormsHPCService;
            _contactUsFormsHumanResourcesService = contactUsFormsHumanResourcesService;
            _contactUsFormsInquiryService = contactUsFormsInquiryService;
            _contactUsFormsRDProjectsService = contactUsFormsRDProjectsService;
            _contactUsFormsSuggestionsService = contactUsFormsSuggestionsService;
            _mapper = mapper;
            _config = config;
            _logger = logger;
        }
        public async Task<bool> SendReplys(SendReplysCommand sendReplysCommand)
        {
            switch (sendReplysCommand.Type)
            {
                case ContactUsFormsEnum.Careers:
                    return await SendCareersReplys();
                case ContactUsFormsEnum.Complains:
                    return await SendComplaintsReplys();
                case ContactUsFormsEnum.Feedbacks:
                    return await SendFeedbacksReplys();
                case ContactUsFormsEnum.HPC:
                    return await SendHPCReplys();
                case ContactUsFormsEnum.HumanResources:
                    return await SendHumanResourcesReplys();
                case ContactUsFormsEnum.Inquiry:
                    return await SendInqueryReplys();
                case ContactUsFormsEnum.RDProjects:
                    return await SendRDProjectsReplys();
                case ContactUsFormsEnum.Suggestions:
                    return await SendSuggestionsReplys();
                default:
                    return false;
            }
        }

        public async Task<bool> SendCareersReplys()
        {
            _logger.LogInformation("Inside SendCareersReplys");
            try
            {
                var query = GetSendReplyQuery();
                var items = _contactUsFormsCareersService.GetAll(query).Items;
                Dictionary<string, string> tempPlaceholder;
                List<EmailAddress> toAddresses;
                foreach (var item in items)
                {
                    tempPlaceholder = new Dictionary<string, string>();
                    tempPlaceholder.Add("#Reply#", item.Reply);
                    tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.CareersEn);
                    tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.CareersAr);
                    toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                    _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                    _contactUsFormsCareersService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
          
        }
        public async Task<bool> SendComplaintsReplys()
        {
            _logger.LogInformation("Inside SendComplaintsReplys");
            try
            {
                var query = GetSendReplyQuery();
                var items = _ContactUsFormsComplaintsService.GetAll(query).Items;
                Dictionary<string, string> tempPlaceholder;
                List<EmailAddress> toAddresses;
                foreach (var item in items)
                {
                    tempPlaceholder = new Dictionary<string, string>();
                    tempPlaceholder.Add("#Reply#", item.Reply);
                    tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.ComplaintsEn);
                    tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.ComplaintsAr);
                    toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                    _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                    _ContactUsFormsComplaintsService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
                }
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                return false;
            }
           
        }
        public async Task<bool> SendFeedbacksReplys()
        {
            _logger.LogInformation("Inside SendFeedbacksReplys");
            try
            {
                var query = GetSendReplyQuery();
                var items = _contactUsFormsFeedbacksService.GetAll(query).Items;
                _logger.LogInformation("no of items: " + items.Count);
                Dictionary<string, string> tempPlaceholder;
                List<EmailAddress> toAddresses;
                foreach (var item in items)
                {
                    tempPlaceholder = new Dictionary<string, string>();
                    tempPlaceholder.Add("#Reply#", item.Reply);
                    tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.FeedbacksEn);
                    tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.FeedbacksAr);
                    toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                    _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                    _contactUsFormsFeedbacksService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
                }
                _logger.LogInformation("End SendFeedbacksReplys");
                return true;
            }
            catch (Exception ex)
            {
               
                _logger.LogInformation(ex.Message);
                return false;
            }
           
        }
        public async Task<bool> SendHPCReplys()
        {
            _logger.LogInformation("Inside SendHPCReplys");
            var query = GetSendReplyQuery();
            var items = _contactUsFormsHPCService.GetAll(query).Items;
            Dictionary<string, string> tempPlaceholder;
            List<EmailAddress> toAddresses;
            foreach (var item in items)
            {
                tempPlaceholder = new Dictionary<string, string>();
                tempPlaceholder.Add("#Reply#", item.Reply);
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.HPCEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.HPCAr);
                toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                _contactUsFormsHPCService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
            }
            return true;
        }
        public async Task<bool> SendHumanResourcesReplys()
        {
            _logger.LogInformation("Inside SendHumanResourcesReplys");
            var query = GetSendReplyQuery();
            var items = _contactUsFormsHumanResourcesService.GetAll(query).Items;
            Dictionary<string, string> tempPlaceholder;
            List<EmailAddress> toAddresses;
            foreach (var item in items)
            {
                tempPlaceholder = new Dictionary<string, string>();
                tempPlaceholder.Add("#Reply#", item.Reply);
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.HumanResourcesEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.HumanResourcesAr);
                toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                _contactUsFormsHumanResourcesService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
            }
            return true;
        }
        public async Task<bool> SendInqueryReplys()
        {
            _logger.LogInformation("Inside SendInqueryReplys");
            var query = GetSendReplyQuery();
            var items = _contactUsFormsInquiryService.GetAll(query).Items;
            Dictionary<string, string> tempPlaceholder;
            List<EmailAddress> toAddresses;
            foreach (var item in items)
            {
                tempPlaceholder = new Dictionary<string, string>();
                tempPlaceholder.Add("#Reply#", item.Reply);
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.InquiryEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.InquiryAr);
                toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                _contactUsFormsInquiryService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
            }
            return true;
        }
        public async Task<bool> SendRDProjectsReplys()
        {
            _logger.LogInformation("Inside SendRDProjectsReplys");
            var query = GetSendReplyQuery();
            var items = _contactUsFormsRDProjectsService.GetAll(query).Items;
            Dictionary<string, string> tempPlaceholder;
            List<EmailAddress> toAddresses;
            foreach (var item in items)
            {
                tempPlaceholder = new Dictionary<string, string>();
                tempPlaceholder.Add("#Reply#", item.Reply);
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.RDProjectsEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.RDProjectsAr);
                toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                _contactUsFormsRDProjectsService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
            }
            return true;
        }
        public async Task<bool> SendSuggestionsReplys()
        {
            _logger.LogInformation("Inside SendSuggestionsReplys");
            var query = GetSendReplyQuery();
            var items = _contactUsFormsSuggestionsService.GetAll(query).Items;
            Dictionary<string, string> tempPlaceholder;
            List<EmailAddress> toAddresses;
            foreach (var item in items)
            {
                tempPlaceholder = new Dictionary<string, string>();
                tempPlaceholder.Add("#Reply#", item.Reply);
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.SuggestionsEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.SuggestionsAr);
                toAddresses = new List<EmailAddress>
                {
                    new EmailAddress {Address= item.Email}
                };
                _sendEmailService.SendMail(toAddresses, "RequestFeedback", MailTypesEnum.RequestFeedback, tempPlaceholder);
                _contactUsFormsSuggestionsService.UpdateField("", Constant.Fields.IsReplySent, item.Id, "1");
            }
            return true;
        }

        public async Task<bool> CreateContactUsForm(CreateContactUsFormCommand createContactUsFormCommand)
        {
            var model = _mapper.Map<CreateContactUsBaseViewModel>(createContactUsFormCommand);
            model.RequestNumber = CreateRequestNumber(model.TypeId);
            switch (model.TypeId)
            {
                case (int)ContactUsFormsEnum.Careers:
                    return await CreateCareerRequest(model);
                case (int)ContactUsFormsEnum.Complains:
                    return await CreateComplaintRequest(model);
                case (int)ContactUsFormsEnum.Feedbacks:
                    return await CreateFeedbackRequest(model);
                case (int)ContactUsFormsEnum.HPC:
                    return await CreateHPCRequest(model);
                case (int)ContactUsFormsEnum.HumanResources:
                    return await CreateHumanResourcesRequest(model);
                case (int)ContactUsFormsEnum.Inquiry:
                    return await CreateInquiryRequest(model);
                case (int)ContactUsFormsEnum.RDProjects:
                    return await CreateRDProjectsRequest(model);
                case (int)ContactUsFormsEnum.Suggestions:
                    return await CreateSuggestionsRequest(model);
                default:
                    return false;
            }
        }
        public async Task<bool> CreateCareerRequest(CreateContactUsBaseViewModel model)
        {
            var result = _contactUsFormsCareersService.AddContactUsItem(model);
            if(result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address= model.Email} };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.CareersEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.CareersAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _contactUsFormsCareersService.GetGroupUsers(GroupsNames.Careers);
                if (adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsCareers) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> CreateComplaintRequest(CreateContactUsBaseViewModel model)
        {
            var result = _ContactUsFormsComplaintsService.AddContactUsItem(model);
            if (result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = model.Email } };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.ComplaintsEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.ComplaintsAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _ContactUsFormsComplaintsService.GetGroupUsers(GroupsNames.Complain);
                if (adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsComplaints) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> CreateFeedbackRequest(CreateContactUsBaseViewModel model)
        {
            var result = _contactUsFormsFeedbacksService.AddContactUsItem(model);
            if (result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = model.Email } };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.FeedbacksEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.FeedbacksAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _contactUsFormsFeedbacksService.GetGroupUsers(GroupsNames.FeedBacksAndSuggestions);
                if (adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsFeedbacks) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> CreateHPCRequest(CreateContactUsBaseViewModel model)
        {
            var result = _contactUsFormsHPCService.AddContactUsItem(model);
            if (result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = model.Email } };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.HPCEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.HPCAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _contactUsFormsHPCService.GetGroupUsers(GroupsNames.HPC);
                if (adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsHPC) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> CreateHumanResourcesRequest(CreateContactUsBaseViewModel model)
        {
            var result = _contactUsFormsHumanResourcesService.AddContactUsItem(model);
            if (result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = model.Email } };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.HumanResourcesEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.HumanResourcesAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _contactUsFormsHumanResourcesService.GetGroupUsers(GroupsNames.HumanResources);
                if (adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsHumanResources) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> CreateInquiryRequest(CreateContactUsBaseViewModel model)
        {
            var result = _contactUsFormsInquiryService.AddContactUsItem(model);
            if (result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = model.Email } };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.InquiryEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.InquiryAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _contactUsFormsInquiryService.GetGroupUsers(GroupsNames.Inquiry);
                if (adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsInquiry) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> CreateRDProjectsRequest(CreateContactUsBaseViewModel model)
        {
            var result = _contactUsFormsRDProjectsService.AddContactUsItem(model);
            if (result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = model.Email } };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.RDProjectsEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.RDProjectsAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _contactUsFormsRDProjectsService.GetGroupUsers(GroupsNames.RDProjects);
                if (adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsRDProjects) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> CreateSuggestionsRequest(CreateContactUsBaseViewModel model)
        {
            var result = _contactUsFormsSuggestionsService.AddContactUsItem(model);
            if (result != -1)
            {
                Dictionary<string, string> tempPlaceholder = new Dictionary<string, string>();
                List<EmailAddress> toAddresses = new List<EmailAddress> { new EmailAddress { Address = model.Email } };
                tempPlaceholder.Add("#TypeEn#", ContactUsFormsTypes.SuggestionsEn);
                tempPlaceholder.Add("#TypeAr#", ContactUsFormsTypes.SuggestionsAr);
                tempPlaceholder.Add("#RefNum#", model.RequestNumber);
                _sendEmailService.SendMail(toAddresses, "SubmissionConfirmed", MailTypesEnum.SubmissionTypeConfirmed, tempPlaceholder);
                //send Admin Email
                var adminEmails = _contactUsFormsSuggestionsService.GetGroupUsers(GroupsNames.FeedBacksAndSuggestions);
                if(adminEmails is not null && adminEmails.Count() > 0)
                {
                    var editURL = _config.GetValue<string>("Sharepoint:EditListItemURL");
                    if (!string.IsNullOrEmpty(editURL))
                    {
                        editURL = editURL.Replace("{listName}", ListsNames.ContactUsSuggestions) + result;
                        tempPlaceholder.Add("#formURL", editURL);
                    }
                    _sendEmailService.SendMail(adminEmails, "NewSubmission", MailTypesEnum.NewSubmission, tempPlaceholder);
                }
                return true;
            }
            return false;
        }

        private Query GetSendReplyQuery()
        {
            var query = new Query
            {
                Filters = new List<Filter>()
            };
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.SendEmail,
                Operator = "Eq",
                Value = "1",
                FieldValueType = "Boolean"
            });
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.IsReplySent,
                Operator = "Eq",
                Value = "0",
                FieldValueType = "Boolean"
            });
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.Reply,
                Operator = "IsNotNull",
                FieldValueType = "Note"
            });
            return query;
        }

        private string CreateRequestNumber(int TypeId)
        {
            string code = string.Empty;
            switch (TypeId)
            {
                case (int)ContactUsFormsEnum.Careers:
                    code = ContactUsTypesCodes.CareersCode;
                    break;
                case (int)ContactUsFormsEnum.Complains:
                    code = ContactUsTypesCodes.ComplaintsCode;
                    break;
                case (int)ContactUsFormsEnum.Feedbacks:
                    code = ContactUsTypesCodes.FeedBackCode;
                    break;
                case (int)ContactUsFormsEnum.HPC:
                    code = ContactUsTypesCodes.HPCCode;
                    break;
                case (int)ContactUsFormsEnum.HumanResources:
                    code = ContactUsTypesCodes.HRCode;
                    break;
                case (int)ContactUsFormsEnum.Inquiry:
                    code = ContactUsTypesCodes.InquiriesCCode;
                    break;
                case (int)ContactUsFormsEnum.RDProjects:
                    code = ContactUsTypesCodes.RDCode;
                    break;
                case (int)ContactUsFormsEnum.Suggestions:
                    code = ContactUsTypesCodes.SuggestionsCCode;
                    break;
                default:
                    break;
            }
            string tick = DateTime.Now.Ticks.ToString();
            code += tick.Substring(tick.Length - 10);
            return code;
        }

    }
    
}
