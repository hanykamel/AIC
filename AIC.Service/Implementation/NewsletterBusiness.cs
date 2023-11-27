using AIC.CrossCutting.Constant;
using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Interfaces;
using AIC.CrossCutting.MailService;
using AIC.CrossCutting.Models.MvcModels;
using AIC.Data.Enums;
using AIC.Data.Model;
using AIC.Data.ViewModels;
using AIC.Repository;
using AIC.Service.Entities.CommonActions.Newsletter.Commands;
using AIC.Service.Interfaces;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware.Models;
using AIC.SP.Middleware.SPViewModels;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIC.Service.Implementation
{
    public class NewsletterBusiness : INewsletterBusiness
    {
        readonly private IRepository<Subscriber, Guid> _subscribersRepository;
        private readonly IMapper _mapper;
        private readonly ICustomCryptography _crypt;
        private readonly IService<NewsViewModel> _newsService;
        private IEmailService _emailService;
        private readonly IConfiguration _config;
        private ICacheService _cacheService;
        private ISendEmail _sendEmailService;
        private readonly IService<MailingListViewModel> _mailingListService;
        readonly private IUnitOfWork _unitOfWork;
        ILogger<NewsletterBusiness> _logger;

        public NewsletterBusiness(IRepository<Subscriber, Guid> subscribersRepository, IMapper mapper,
            ICustomCryptography crypt, IEmailService emailService,
               IService<NewsViewModel> newService,
                IConfiguration config, ICacheService cacheService
            , ISendEmail sendEmailService, IService<MailingListViewModel> mailingListService
            , IUnitOfWork unitOfWork, ILogger<NewsletterBusiness> logger)
        {
            _unitOfWork = unitOfWork;
            _subscribersRepository = subscribersRepository;
            _mapper = mapper;
            _crypt = crypt;
            _newsService = newService;
            _emailService = emailService;
            _config = config;
            _cacheService = cacheService;
            _sendEmailService = sendEmailService;
            _mailingListService = mailingListService;
            _logger = logger;
        }

        public async Task<bool> AddSubscriber(SubscriberViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("NullModel");
            var duplicateEntity = _subscribersRepository.GetFirst(g => g.Email.ToLower() == model.Email.ToLower(), true);
            if (duplicateEntity != null)
            {
                if (duplicateEntity.IsDeleted != true)
                    throw new DuplicatedItemException("AlreadySubscribed");
                else
                {
                    duplicateEntity.SubscriptionDate = DateTime.Now;
                    duplicateEntity.IsDeleted = false;
                    _subscribersRepository.Update(duplicateEntity);
                }
            }
            else
            {
                var subscriber = _mapper.Map<Subscriber>(model);
                _subscribersRepository.Add(subscriber);
            }
            return await _unitOfWork.SaveChangesAsync() > 0;

        }


        public async Task<bool> UnSubscrib(string email)
        {
            string decryptedEmail = _crypt.Decrypt(email);
            Subscriber subscriber = _subscribersRepository.GetFirst(g => g.Email.ToLower() == decryptedEmail.ToLower(), true);
            if (subscriber == null)
                throw new UserNotFoundException("NotSubscribedBefore");
            if (subscriber.IsDeleted == true)
                throw new UserAlreadyUnsubscribedException("AlreadyUnSubscribed");

            var subscriberEntity = _mapper.Map<Subscriber>(subscriber);
            subscriber.SubscriptionDate = null;
            subscriber.IsDeleted = true;
            _subscribersRepository.Update(subscriber);

            return await _unitOfWork.SaveChangesAsync() > 0;

        }

        public ListSubscribersViewModel Get(GetAllSubscriberCommand query)
        {
            ListFilter titleFilter = null;
            if (query.Filters != null && query.Filters.Count() > 0)
            {
                titleFilter = query.Filters.Where(t => t.Field == "Email").FirstOrDefault();
            }
            var subscribers = _subscribersRepository.GetAll()
            .Where(t => titleFilter != null ? t.Email.ToLower().Contains(titleFilter.Value.ToLower()) : true).OrderByDescending(s => s.SubscriptionDate);
            List<Subscriber> PagedSubscribers = subscribers.Skip(query.PageSize * query.PageIndex).Take(query.PageSize).ToList();
            int count = subscribers.Count();
            var mappedSubscribers = _mapper.Map<List<SubscriberVM>>(PagedSubscribers);

            ListSubscribersViewModel lookupsViewModelList = new ListSubscribersViewModel { List = mappedSubscribers, TotalCount = count };
            return lookupsViewModelList;

        }

        public async Task<bool> SendNewsLetter()
        {
            string fileURL = "";
            _logger.LogInformation("Inside send newsletter");
            //var mailSubject = "MailingList";
            var mailingList = GetMailingList();
            Dictionary<string, string> tempPlaceholder;
            if (mailingList is not null && mailingList.Count() > 0)
            {
                var subscribers = _subscribersRepository.GetAll();
                //var toAddresses = _mapper.Map<List<EmailAddress>>(subscribers);
                List<EmailAddress> toAddresses;
                foreach (var mail in mailingList)
                {
                    foreach (var subscriber in subscribers)
                    {
                        toAddresses = new List<EmailAddress> { new EmailAddress { Address = subscriber.Email } };
                        tempPlaceholder = new Dictionary<string, string>();
                        tempPlaceholder.Add("#MailingListEnBody#", mail.Body);
                        tempPlaceholder.Add("#MailingAr​ListBody#", mail.BodyAr);
                        _logger.LogInformation("PDF url: " + mail.Attachments);
                        if (mail.Attachments is not null && mail.Attachments != "http://" && mail.Attachments != "https://")
                            fileURL = mail.Attachments;
                        //if (mail.Attachments is not null && mail.Attachments != "http://" && mail.Attachments != "https://")
                        //{
                        //    fileURL = mail.Attachments;
                        //    _logger.LogInformation("replace pdf file placeholder");
                        //    tempPlaceholder.Add("#FileURL", mail.Attachments);
                        //    tempPlaceholder.Add("​#PDFNameEn#", "Download File");
                        //    tempPlaceholder.Add("​#PDFNameAr#", "تحميل الملف");
                        //    _logger.LogInformation("url placeholder: " + tempPlaceholder["#FileURL"]);
                        //}
                        //else
                        //{
                        //    tempPlaceholder.Add("​#PDFNameEn#", "");
                        //    tempPlaceholder.Add("​#PDFNameAr#", "");
                        //}
                        string unsubscribeURL = _config.GetValue<string>("AngularComponents:Unsubscribe").Replace("{email}", _crypt.Encrypt(subscriber.Email));
                        tempPlaceholder.Add("#UnsubscribeEnURLPlaceHolder", unsubscribeURL + "en");
                        tempPlaceholder.Add("#UnsubscribeArURLPlaceHolder", unsubscribeURL + "ar");
                        _sendEmailService.SendMail(toAddresses, mail.Title, MailTypesEnum.Newsletter, tempPlaceholder, fileURL);
                    }


                }
            }
            return true;
        }

        private List<MailingListViewModel> GetMailingList()
        {
            List<MailingListViewModel> mailingList;
            DateTime currentDate = DateTime.Now;
            var query = new Query
            {
                Filters = new List<Filter>()
            };
            query.Filters = new List<Filter>();
            query.Filters.Add(new Filter
            {
                Field = Constant.Fields.Date,
                Operator = "Eq",
                Value = currentDate.ToString("yyyy-MM-dd"),
                FieldValueType = "DateTime"
            });
            mailingList = _mailingListService.GetAllWithAttachments(query).Items;
            return mailingList;
        }

    }
}
