
using AIC.SP.Middleware.SPViewModels;
using Asset.SharePoint.Middleware.Helpers;
using AutoMapper;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.SP.Middleware.Mapper
{
    public class AutoMapperConfig
    {

        public static IMapper Mapper { get; private set; }
        public static void Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                CreateMapper(cfg);
            });
            Mapper = configuration.CreateMapper();
        }

        private static void CreateMapper(IMapperConfigurationExpression cfg)
        {
            CreateNewsMapper(cfg);
            CreateCacheMapper(cfg);
            CreateFAQMapper(cfg);
            CreateVideoAlbumMapper(cfg);
            CreatePhotoAlbumMapper(cfg);
            CreatePhotoMapper(cfg);
            CreateVideoMapper(cfg);
            CreateProjectsMapper(cfg);
            CreateApplicationsDomainMapper(cfg);
            CreateTechnologyDomainsMapper(cfg);
            // CreateAttachmentMapper(cfg);
            CreatePartenershipsMapper(cfg);
            CreateHomePageSectionsMapper(cfg);
            CreateHighlightsNewsMapper(cfg);
            CreateHighlightsEventsMapper(cfg);
            CreateCareersMapper(cfg);
            CreateOurPeopleMapper(cfg);
            CreateBodAndScientific(cfg);
            CreateMainMenuMapper(cfg);
            CreateSocialMediaLinksMapper(cfg);
            CreateMainBannerMapper(cfg);
            CreateGenericContentListMapper(cfg);
            CreateHPCProjectsMapper(cfg);
            CreateSocialMediaFeedsMapper(cfg);
            CreateBestPracticeMapper(cfg);
            CreateWhitePapersMapper(cfg);
            CreateAICMaterialsSubjectsMapper(cfg);
            CreateAICMaterialsMapper(cfg);
            CreateAICMaterialVideoMapper(cfg);
            CreateMediaLinksMapper(cfg);
            CreateEventsMapper(cfg);
            CreatePortalPagesMapper(cfg);
            CreateInternshipsMapper(cfg);
            CreateContactUsMapper(cfg);
            CreateEmailsMapper(cfg);
            CreateMailingListMapper(cfg);
            CreateContactUsCareersMapper(cfg);
            CreateContactUsComplaintsMapper(cfg);
            CreateContactUsFeedbacksMapper(cfg);
            CreateContactUsHPCMapper(cfg);
            CreateContactUsHumanResourcesMapper(cfg);
            CreateContactUsInquiryMapper(cfg);
            CreateContactUsRDProjectsMapper(cfg);
            CreateApplicationDomainCacheMapper(cfg);
            CreateTechnologyDomainCacheMapper(cfg);
            CreateContactUsSuggestionsMapper(cfg);
            CreateProjectsCacheMapper(cfg);
            CreateBestPracticeCacheMapper(cfg);
            CreateBodAndScientificCacheMapper(cfg);
            CreateOurPeopleCacheMapper(cfg);
            CreatePhotoAlbumCacheMapper(cfg);
            CreateVideoAlbumCacheMapper(cfg);
            CreateGenericContentCacheMapper(cfg);
            CreateAicMaterialsCacheMapper(cfg);
            CreateHPCProjectsCacheMapper(cfg);
            CreateNewsCacheMapper(cfg);
            CreateEventsCacheMapper(cfg);
            CreateCareersCacheMapper(cfg);
            CreateInternshipsCacheMapper(cfg);
            CreateWhitePapersCacheMapper(cfg);
            CreateCountriesMapper(cfg);
            CreateCitiesMapper(cfg);
            CreateCareersARMapper(cfg);
            CreateCareersENMapper(cfg);
            CreateCareersCacheENMapper(cfg);
            CreateCareersCacheARMapper(cfg);
            CreateInternShipsARMapper(cfg);
            CreateInternShipsENMapper(cfg);
            CreateInternshipsARCacheMapper(cfg);
            CreateInternshipsENCacheMapper(cfg);
        }

        private static void CreateCitiesMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, CityViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])));
            ;
        }
        private static void CreateCountriesMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, CountryViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])));
                ;
        }
        private static void CreateWhitePapersCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<WhitePaperViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.Brief))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.Date));
                          
        }
        private static void CreateInternshipsCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<InternshipsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.InternshipOverview))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PublishingDate != null ? e.PublishingDate.Value : e.ModifiedDate));
        }
        private static void CreateCareersCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CareersViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.JobOverview))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PublishingDate != null ? e.PublishingDate.Value : e.ModifiedDate));
        }
        private static void CreateEventsCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EventsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.AICBrief))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.EventDate));
        }
        private static void CreateNewsCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<NewsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.AICBrief))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PublishDate));
        }
        private static void CreateHPCProjectsCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<HPCProjectsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.AICBrief))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.ModifiedDate));

        }
        private static void CreateProjectsCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProjectsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.ShortDescription))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.ModifiedDate))
                           .ForMember(p => p.DomainName, src => src.MapFrom(e => e.ApplicationDomain))
                           .ForMember(p => p.CategoryName, src => src.MapFrom(e => e.TechnologyDomain));

        }
        private static void CreateBestPracticeCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BestPracticeViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.Brief))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PractiseDate));
        }
        private static void CreateBodAndScientificCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BodAndScientificViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.AICBrief))
                           .ForMember(p => p.DemoBrief, src => src.MapFrom(e => e.Name));

        }
        private static void CreateOurPeopleCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OurPeopleViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.DemoBrief, src => src.MapFrom(e => e.Name))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.AICBrief));

        }
        private static void CreatePhotoAlbumCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PhotoAlbumViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.Brief))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.AlbumDate));

        }
        private static void CreateVideoAlbumCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<VideoAlbumViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.Brief))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.AlbumDate));

        }
        private static void CreateGenericContentCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<GenericContentViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Description, src => src.MapFrom(e => e.AICDesc));

        }
        private static void CreateAicMaterialsCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AICMaterialsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.Brief));

        }
        private static void CreateApplicationDomainCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ApplicationsDomainsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title));
        }
        private static void CreateTechnologyDomainCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TechnologyDomainsViewModel, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title));
        }
        private static void CreateContactUsSuggestionsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsFormsSuggestionsViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateContactUsRDProjectsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsRDProjectsViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateContactUsInquiryMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsFormsInquiryViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateContactUsHumanResourcesMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsFormsHumanResourcesViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateContactUsHPCMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsFormsHPCViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateContactUsFeedbacksMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsFormsFeedbacksViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateContactUsComplaintsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsFormsComplaintsViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateContactUsCareersMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsFormsCareersViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Email, src => src.MapFrom(e => Convert.ToString(e["AICEmail"])))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["AICPhoneNumber"])))
                .ForMember(dst => dst.SubmittedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                .ForMember(dst => dst.Country, src => src.MapFrom(e => e["Country"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Country") : ""))
                .ForMember(dst => dst.City, src => src.MapFrom(e => e["AICCity"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICCity") : ""))
                .ForMember(dst => dst.WorkEducationalOrganization, src => src.MapFrom(e => Convert.ToString(e["AICWorkEducationalOrganization"])))
                .ForMember(dst => dst.Message, src => src.MapFrom(e => Convert.ToString(e["AICMessage"])))
                .ForMember(dst => dst.Reply, src => src.MapFrom(e => Convert.ToString(e["AICReply"])))
                .ForMember(dst => dst.SendEmail, src => src.MapFrom(e => e["AICSendEmail"] != null ? Convert.ToBoolean(e["AICSendEmail"]) : e["AICSendEmail"]))
                .ForMember(dst => dst.IsReplySent, src => src.MapFrom(e => e["AICIsReplySent"] != null ? Convert.ToBoolean(e["AICIsReplySent"]) : e["AICIsReplySent"]));
        }
        private static void CreateMailingListMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, MailingListViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Body, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
                .ForMember(dst => dst.BodyAr, src => src.MapFrom(e => Convert.ToString(e["BodyAr"])))
                .ForMember(dst => dst.Attachments, src => src.MapFrom(e => e["FileUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "FileUrl") : null))
                .ForMember(dst => dst.Date, src => src.MapFrom(e => e["Date"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Date"]))) : e["Date"]));
        }
        private static void CreateEmailsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, MailsViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Content, src => src.MapFrom(e => Convert.ToString(e["AICEmailContent"])))
                .ForMember(dst => dst.Placeholders, src => src.MapFrom(e => Convert.ToString(e["AICEmailPlaceholders"])))
                .ForMember(dst => dst.MailType, src => src.MapFrom(e => e["MailType"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "MailType") : ""))
                ;
        }
        private static void CreateInternshipsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, InternshipsViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])))
                .ForMember(p => p.ReferenceNumber, src => src.MapFrom(e => Convert.ToString(e["AICInternshipReferenceNumber"])))
                .ForMember(p => p.PublishingDate, src => src.MapFrom(e => e["AICDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"]))) : e["AICDate"]))
                .ForMember(p => p.ProjectDepartment, src => src.MapFrom(e => e["AICProjectDepartment"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICProjectDepartment") : ""))
                .ForMember(p => p.ProjectDepartmentAr, src => src.MapFrom(e => e["Project_x002f_Department_x003a_T"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Project_x002f_Department_x003a_T") : ""))
                .ForMember(p => p.InternshipOverview, src => src.MapFrom(e => Convert.ToString(e["AICInternshipOverview"])))
                .ForMember(p => p.InternshipOverviewAr, src => src.MapFrom(e => Convert.ToString(e["AICInternshipOverviewAr"])))
                .ForMember(p => p.Description, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
                .ForMember(p => p.DescriptionAr, src => src.MapFrom(e => Convert.ToString(e["AICDescAr"])))
                .ForMember(p => p.InternshipQualifications, src => src.MapFrom(e => Convert.ToString(e["AICInternshipQualifications"])))
                .ForMember(p => p.InternshipQualificationsAr, src => src.MapFrom(e => Convert.ToString(e["AICInternshipQualificationsAr"])))
                .ForMember(p => p.InternshipRequirements, src => src.MapFrom(e => Convert.ToString(e["AICInternshipRequirements"])))
                .ForMember(p => p.InternshipRequirementsAr, src => src.MapFrom(e => Convert.ToString(e["AICInternshipRequirementsAr"])))
                .ForMember(p => p.Location, src => src.MapFrom(e => e["AICLocation"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICLocation") : ""))
                .ForMember(p => p.LocationAr, src => src.MapFrom(e => e["Location_x003a_Title_x0020_in_x0"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Location_x003a_Title_x0020_in_x0") : ""))
                .ForMember(p => p.ModifiedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Modified"])))))
                .ForMember(p => p.ExpiryDate, src => src.MapFrom(e => e["AICInternshipExpiryDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICInternshipExpiryDate"]))) : e["AICInternshipExpiryDate"]));
        }
        private static void CreateContactUsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ContactUsViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])))
                .ForMember(dst => dst.Address, src => src.MapFrom(e => Convert.ToString(e["Address"])))
                .ForMember(dst => dst.AddressAr, src => src.MapFrom(e => Convert.ToString(e["AddressinArabic"])))
                .ForMember(dst => dst.Lat, src => src.MapFrom(e => Convert.ToDouble(e["AICLatitude"])))
                .ForMember(dst => dst.Long, src => src.MapFrom(e => Convert.ToDouble(e["AICLongitude"])))
                .ForMember(dst => dst.Location, src => src.MapFrom(e => e["Location"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "Location") : ""))
                .ForMember(dst => dst.PhoneNumber, src => src.MapFrom(e => Convert.ToString(e["PhoneNumber"])))
                .ForMember(dst => dst.IsHeadOffice, src => src.MapFrom(e => e["HeadOffice"] != null ? Convert.ToBoolean(e["HeadOffice"]) : e["HeadOffice"]));
        }
        private static void CreatePortalPagesMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, PortalPagesViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Url, src => src.MapFrom(e => e["AICUrl"] != null ? SharepointFieldHelpers.GetRelativeURLFieldValue(e, "AICUrl") : ""));
        }
        private static void CreateMediaLinksMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, MediaLinksViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])))
                .ForMember(dst => dst.Url, src => src.MapFrom(e => e["AICUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICUrl") : ""));
        }
        private static void CreateBestPracticeMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, BestPracticeViewModel>()
                .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                .ForMember(dst => dst.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                .ForMember(dst => dst.Description, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
                .ForMember(dst => dst.VideoAlbum, src => src.MapFrom(e => e["Videos"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Videos") : ""))
                .ForMember(dst => dst.PhotoAlbum, src => src.MapFrom(e => e["Photos"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Photos") : ""))
                .ForMember(p => p.PractiseDate, src => src.MapFrom(e => e["AICDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"]))): (DateTime?) null));

        }

        private static void CreateAICMaterialsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, AICMaterialsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.SubjectId, src => src.MapFrom(e =>  SharepointFieldHelpers.getLookupFieldId(e, "Subject")))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.Date, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                           .ForMember(p => p.Url, src => src.MapFrom(e => SharepointFieldHelpers.GetURLFieldValue(e, "AICUrl")))
                           .ForMember(p => p.VideoId, src => src.MapFrom(e => e["AICVideo"] != null ? SharepointFieldHelpers.getLookupFieldId(e, "AICVideo").ToString() : ""))
                           .ForMember(p => p.DocumentUrl, src => src.MapFrom(e => e["AICDocumentUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICDocumentUrl") : ""));
        }
        private static void CreateAICMaterialsSubjectsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, AICMaterialsSubjectsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.VideoAlbum, src => src.MapFrom(e => e["Videos"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Videos") : ""));
        }
        private static void CreateWhitePapersMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, WhitePaperViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.Type, src => src.MapFrom(e => e["AICWhitePaperType"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICWhitePaperType") : ""))
                           .ForMember(p => p.Date, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                           .ForMember(p => p.Url, src => src.MapFrom(e => e["AICUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICUrl") : ""))
                           .ForMember(p => p.Description, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])));
        }
        private static void CreateSocialMediaLinksMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, SocialMediaViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.Url, src => src.MapFrom(e => e["URL"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "URL") : ""))
                           .ForMember(p => p.Logo, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))))
                           .ForMember(p => p.HomeLogo, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["Logo"]))));
        }
        private static void CreateMainBannerMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, MainBannerViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.ReadMoreUrl, src => src.MapFrom(e => e["AICUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICUrl") : ""))
                           .ForMember(p => p.VideoUrl, src => src.MapFrom(e => e["AICPlayVideoUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICPlayVideoUrl") : ""));
        }
        private static void CreateMainMenuMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, MainMenuViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICMenuArName"])))
                           .ForMember(p => p.Parent, src => src.MapFrom(e => e["AICMenuParent"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICMenuParent") : ""))
                           .ForMember(p => p.HasChildren, src => src.MapFrom(e => Convert.ToBoolean(e["AICHasChildren"])))
                           .ForMember(p => p.Url, src => src.MapFrom(e => e["AICMenuURL"] != null ? SharepointFieldHelpers.getLookupFieldId(e, "AICMenuURL").ToString() : ""))
                           .ForMember(p => p.OtherUrl, src => src.MapFrom(e => e["AICUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICUrl") : ""))
                           .ForMember(p => p.Order, src => src.MapFrom(e => Convert.ToInt16(e["AICMenuOrder"])))
                           .ForMember(p => p.Hidden, src => src.MapFrom(e => Convert.ToBoolean(e["AICMenuHide"])))
                           .ForMember(p => p.ShowInFooter, src => src.MapFrom(e => Convert.ToBoolean(e["ShowInFooter"])));
        }
        private static void CreateCareersMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, CareersViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])))
                           .ForMember(p => p.ReferenceNumber, src => src.MapFrom(e => Convert.ToString(e["AICJobReferenceNumber"])))
                           .ForMember(p => p.PublishingDate, src => src.MapFrom(e => e["AICDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"]))) : e["AICDate"]))
                           .ForMember(p => p.JobType, src => src.MapFrom(e => e["AICJobType"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICJobType") : ""))
                           .ForMember(p => p.JobTypeAr, src => src.MapFrom(e => e["Job_x0020_Type_x003a_Title_x0020"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Job_x0020_Type_x003a_Title_x0020") : ""))
                           .ForMember(p => p.ReportsTo, src => src.MapFrom(e => Convert.ToString(e["AICReportsTo"])))
                           .ForMember(p => p.ReportsToAr, src => src.MapFrom(e => Convert.ToString(e["AICReportsToAr"])))
                           .ForMember(p => p.JobOverview, src => src.MapFrom(e => Convert.ToString(e["AICJobOverview"])))
                           .ForMember(p => p.JobOverviewAr, src => src.MapFrom(e => Convert.ToString(e["AICJobOverviewAr"])))
                           .ForMember(p => p.Description, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
                           .ForMember(p => p.DescriptionAr, src => src.MapFrom(e => Convert.ToString(e["AICDescAr"])))
                           .ForMember(p => p.JobQualifications, src => src.MapFrom(e => Convert.ToString(e["AICJobQualifications"])))
                           .ForMember(p => p.JobQualificationsAr, src => src.MapFrom(e => Convert.ToString(e["AICJobQualificationsAr"])))
                           .ForMember(p => p.JobRequirements, src => src.MapFrom(e => Convert.ToString(e["AICJobRequirements"])))
                           .ForMember(p => p.JobRequirementsAr, src => src.MapFrom(e => Convert.ToString(e["AICJobRequirementsAr"])))
                           .ForMember(p => p.Location, src => src.MapFrom(e => e["AICLocation"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICLocation") : ""))
                           .ForMember(p => p.LocationAr, src => src.MapFrom(e => e["Location_x003a_Title_x0020_in_x0"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "Location_x003a_Title_x0020_in_x0") : ""))
                           .ForMember(p => p.ModifiedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Modified"])))))
                           .ForMember(p => p.VacancyExpiryDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICVacancyExpiryDate"])))))
                           
                           .ForMember(p => p.ShowInHomePage, src => src.MapFrom(e => Convert.ToBoolean(e["AICShowInHomePage"])));
        }
        private static void CreateHighlightsNewsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, HighlightsNewsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.ThumbnailImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICThumbnailImage"]))))
                           .ForMember(p => p.ModifiedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Modified"])))))
                           .ForMember(p => p.HighlightDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))));
        }
        private static void CreateHighlightsEventsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, HighlightsEventsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.ThumbnailImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICThumbnailImage"]))))
                           .ForMember(p => p.ModifiedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Modified"])))))
                           .ForMember(p => p.HighlightDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))));
        }
        private static void CreateHomePageSectionsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, HomePageSectionsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.FeaturedCareersBrief, src => src.MapFrom(e => Convert.ToString(e["AICFeaturedCareersBrief"])))
                           .ForMember(p => p.ContactUsBrief, src => src.MapFrom(e => Convert.ToString(e["ContactUsBrief"])));
        }

        private static void CreateNewsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, NewsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.MainImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))))
                           .ForMember(p => p.ThumbnailImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICThumbnailImage"]))))
                           .ForMember(p => p.AICBrief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.NewsType, src => src.MapFrom(e => e["AICNewsType"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICNewsType") : ""))
                           .ForMember(p => p.Sector, src => src.MapFrom(e => e["AICNewsSector"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICNewsSector") : ""))
                           .ForMember(p => p.Technology, src => src.MapFrom(e => Convert.ToString(e["AICNewsTechnology"])))
                           .ForMember(p => p.PublishDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                           .ForMember(p => p.Source, src => src.MapFrom(e => Convert.ToString(e["AICSource"])))
                           .ForMember(p => p.SourceUrl, src => src.MapFrom(e => e["AICUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICUrl") : ""))
                           .ForMember(p => p.Description, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
                           .ForMember(p => p.RelatedMediaLinks, src => src.MapFrom(e => e["AICRelatedMediaLinks"] != null ? SharepointFieldHelpers.getMultiLookupField(e, "AICRelatedMediaLinks") : null))
                           .ForMember(p => p.ShowInHomePage, src => src.MapFrom(e => Convert.ToBoolean(e["AICShowInHomePage"])))
                           .ForMember(p => p.ArchiveDate, src => src.MapFrom(e => e["ArchiveDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["ArchiveDate"]))) : (DateTime?)null));

        }
        private static void CreateProjectsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ProjectsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.ShortDescription, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.Description, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
                           .ForMember(p => p.ShowInHomePage, src => src.MapFrom(e => Convert.ToBoolean(e["AICShowInHomePage"])))
                           .ForMember(p => p.ApplicationDomain, src => src.MapFrom(e => e["AICApplicationDomain"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICApplicationDomain") : ""))
                           .ForMember(p => p.TechnologyDomain, src => src.MapFrom(e => e["AICTechnologyDomain"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICTechnologyDomain") : ""))
                           .ForMember(p => p.URL, src => src.MapFrom(e => e["AICUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AICUrl") : ""))
                           .ForMember(p => p.RelatedMediaLinks, src => src.MapFrom(e => e["AICRelatedMediaLinks"] != null ? SharepointFieldHelpers.getMultiLookupField(e, "AICRelatedMediaLinks") : null))
                           .ForMember(p => p.DemoPhotos, src => src.MapFrom(e => e["AICDemoPhotos"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICDemoPhotos") : ""))
                           .ForMember(p => p.DemoVideos, src => src.MapFrom(e => e["AICDemoVideos"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICDemoVideos") : ""))
                           .ForMember(p => p.DemoBrief, src => src.MapFrom(e => Convert.ToString(e["DemoBrief"])))
                           .ForMember(p => p.ImagesTitle, src => src.MapFrom(e => Convert.ToString(e["ImagesTitle"])))
                           .ForMember(p => p.VideosTitle, src => src.MapFrom(e => Convert.ToString(e["VideosTitle"])))
                           .ForMember(p => p.MainImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))))
                           .ForMember(p => p.ThumbnailImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICThumbnailImage"]))))
                           .ForMember(p => p.ModifiedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Modified"])))))
                           .ForMember(p => p.CreatedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Created"])))));
        }
        private static void CreateApplicationsDomainMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, ApplicationsDomainsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])));
        }
        private static void CreateTechnologyDomainsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, TechnologyDomainsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.TitleAr, src => src.MapFrom(e => Convert.ToString(e["AICTitleAr"])));
        }

        private static void CreatePartenershipsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, PartenershipsViewModel>()
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(dst => dst.Url, src => src.MapFrom(e => SharepointFieldHelpers.GetURLFieldValue(e, "URL")))
             .ForMember(p => p.Type, src => src.MapFrom(e => e["PartnersType"] != null ? e["PartnersType"] : ""))
             .ForMember(p => p.Logo, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))));
        }

        private static void CreateBodAndScientific(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, BodAndScientificViewModel>()
             .ForMember(dst => dst.Name, src => src.MapFrom(e => Convert.ToString(e["name"])))
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(dst => dst.AICBrief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
             .ForMember(p => p.AICImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))));


        }


        private static void CreateFAQMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, FAQViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => e["Title"] != null ? Convert.ToString(e["Title"]):""))
                           .ForMember(p => p.TopicTitle, src => src.MapFrom(e => e["AICFAQTopic"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICFAQTopic") : ""))
                           .ForMember(p => p.Answer, src => src.MapFrom(e => Convert.ToString(e["Answer"])));
        }
        private static void CreateCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<NinjaNye.SearchExtensions.Models.IRanked<CacheViewModel>, CacheViewModel>()
                            .ForMember(dst => dst.Id, src => src.MapFrom(e => e.Item.Id))
                            .ForMember(dst => dst.Title, src => src.MapFrom(e => e.Item.Title))
                            .ForMember(dst => dst.Brief, src => src.MapFrom(e => e.Item.Brief))
                            .ForMember(dst => dst.Date, src => src.MapFrom(e => e.Item.Date))
                            .ForMember(dst => dst.Description, src => src.MapFrom(e => e.Item.Description))
                            .ForMember(dst => dst.ListName, src => src.MapFrom(e => e.Item.ListName))
                            .ForMember(dst => dst.Lang, src => src.MapFrom(e => e.Item.Lang));
        }
        private static void CreatePhotoAlbumMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, PhotoAlbumViewModel>()
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(dst => dst.AlbumDate, src => src.MapFrom(e => e["AICDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"]))): e["AICDate"]))
             .ForMember(dst => dst.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
             .ForMember(dst => dst.FileRef, src => src.MapFrom(e => Convert.ToString(e["FileLeafRef"])))
             .ForMember(dst => dst.AlbumCoverImage, src => src.MapFrom(e => SharepointFieldHelpers.GetURLFieldValue(e, "AICImageUrl")));
        }

        private static void CreateVideoAlbumMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, VideoAlbumViewModel>()
             .ForMember(dst => dst.Id, src => src.MapFrom(e => e.Id))
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(dst => dst.AlbumDate, src => src.MapFrom(e => e["AICDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"]))) : e["AICDate"]))
             .ForMember(dst => dst.FileRef, src => src.MapFrom(e => Convert.ToString(e["FileLeafRef"])))
             .ForMember(dst => dst.Brief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
             .ForMember(dst => dst.AlbumCoverImage, src => src.MapFrom(e => SharepointFieldHelpers.GetURLFieldValue(e, "AICImageUrl")));
        }

        private static void CreateVideoMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, VideoViewModel>()
            .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
            .ForMember(dst => dst.Description, src => src.MapFrom(e => Convert.ToString(e["VideoSetDescription"])))
            .ForMember(dst => dst.EmbedCode, src => src.MapFrom(e => Convert.ToString(e["VideoSetEmbedCode"])))
            .ForMember(dst => dst.URL, src => src.MapFrom(e => e["VideoSetDefaultEncoding"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "VideoSetDefaultEncoding") : Convert.ToString(((FieldUrlValue)e["VideoSetExternalLink"]).Url)))
            .ForMember(dst => dst.ThumbURL, src => src.MapFrom(e => e["AlternateThumbnailUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AlternateThumbnailUrl") : ""))
            .ForMember(dst => dst.VideoDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Created"])))))
            .ForMember(dst => dst.Src, src => src.MapFrom(e => Convert.ToString(e["VideoSetEmbedCode"]) != null && Convert.ToString(e["VideoSetEmbedCode"]) != "" ? Convert.ToString(e["VideoSetEmbedCode"]) : (e["VideoSetDefaultEncoding"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "VideoSetDefaultEncoding") : Convert.ToString(((FieldUrlValue)e["VideoSetExternalLink"]).Url))))
            .ForMember(dst => dst.Duration, src => src.MapFrom(e => TimeSpan.FromSeconds(Convert.ToDouble(e["MediaLengthInSeconds"])).ToString(@"mm\:ss")));
        }
        private static void CreateAICMaterialVideoMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, AICMaterialsVideosViewModel>()
            .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
            .ForMember(dst => dst.Description, src => src.MapFrom(e => Convert.ToString(e["VideoSetDescription"])))
            .ForMember(dst => dst.EmbedCode, src => src.MapFrom(e => Convert.ToString(e["VideoSetEmbedCode"])))
            .ForMember(dst => dst.URL, src => src.MapFrom(e => e["VideoSetDefaultEncoding"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "VideoSetDefaultEncoding") : Convert.ToString(((FieldUrlValue)e["VideoSetExternalLink"]).Url)))
            .ForMember(dst => dst.ThumbURL, src => src.MapFrom(e => e["AlternateThumbnailUrl"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "AlternateThumbnailUrl") : ""))
            .ForMember(dst => dst.VideoDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Created"])))))
            .ForMember(dst => dst.Src, src => src.MapFrom(e => Convert.ToString(e["VideoSetEmbedCode"]) != null && Convert.ToString(e["VideoSetEmbedCode"]) != "" ? Convert.ToString(e["VideoSetEmbedCode"]) : (e["VideoSetDefaultEncoding"] != null ? SharepointFieldHelpers.GetURLFieldValue(e, "VideoSetDefaultEncoding") : Convert.ToString(((FieldUrlValue)e["VideoSetExternalLink"]).Url))))
            .ForMember(dst => dst.Duration, src => src.MapFrom(e => TimeSpan.FromSeconds(Convert.ToDouble(e["MediaLengthInSeconds"])).ToString(@"mm\:ss")));
        }
        private static void CreatePhotoMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, PhotoViewModel>()
                .ForMember(dst => dst.FileRef, src => src.MapFrom(e => Convert.ToString(SharepointFieldHelpers.PublicURL(Convert.ToString(e["FileRef"])))))
                .ForMember(dst => dst.name, src => src.MapFrom(e => Convert.ToString(e["FileLeafRef"])))
                 .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])));

        }
        //private static void CreateAttachmentMapper(IMapperConfigurationExpression cfg)
        //{
        //    cfg.CreateMap<ListItem, ActivitiesAttachmentViewModel>()
        //            .ForMember(p => p.Title, src => src.MapFrom(e => e.AttachmentFiles != null && e.AttachmentFiles.Count > 0 ? Convert.ToString(e.AttachmentFiles[0].FileName) : Convert.ToString(e["Title"])))
        //            .ForMember(p => p.AttachmentBytes, src => src.MapFrom(e => e["File_x0020_Type"] != null ? e["File_x0020_Type"] : null))
        //            .ForMember(p => p.Id, src => src.MapFrom(e => e.Id));
        //}

        private static void CreateOurPeopleMapper (IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem , OurPeopleViewModel>()
             .ForMember(dst => dst.Name, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["AICTitle"])))
             .ForMember(dst => dst.AICBrief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
             .ForMember(p => p.AICImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))));

        }
        private static void CreateGenericContentListMapper(IMapperConfigurationExpression cfd)
        {
            cfd.CreateMap<ListItem , GenericContentViewModel>()
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(dst => dst.AICDesc, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
             .ForMember(dst => dst.GenericContentType, src => src.MapFrom(e => Convert.ToString(e["AICGenericContentType"])))
             .ForMember(p => p.AICImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))));
        }
        private static void CreateSocialMediaFeedsMapper(IMapperConfigurationExpression cfd)
        {
            cfd.CreateMap<ListItem, SocialMediaFeedViewModel>()
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(p => p.AICSocialMediaType, src => src.MapFrom(e => e["AICSocialMediaType"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICSocialMediaType") : ""))
             .ForMember(dst => dst.EmbedCode, src => src.MapFrom(e => Convert.ToString(e["AICEmbeddedURL"])));
        }
        private static void CreateHPCProjectsMapper(IMapperConfigurationExpression cfd)
        {
            cfd.CreateMap<ListItem, HPCProjectsViewModel>()
             .ForMember(dst => dst.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
             .ForMember(dst => dst.AICDesc, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
             .ForMember(dst => dst.AICBrief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
             .ForMember(p => p.ModifiedDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["Modified"])))))
             .ForMember(p => p.AICImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))));

        }
        private static void CreateEventsMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ListItem, EventsViewModel>()
                           .ForMember(p => p.Title, src => src.MapFrom(e => Convert.ToString(e["Title"])))
                           .ForMember(p => p.MainImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICImage"]))))
                           .ForMember(p => p.ThumbnailImage, src => src.MapFrom(e => SharepointFieldHelpers.GetPublishingPageImageURL(Convert.ToString(e["AICThumbnailImage"]))))
                           .ForMember(p => p.AICBrief, src => src.MapFrom(e => Convert.ToString(e["AICBrief"])))
                           .ForMember(p => p.EventType, src => src.MapFrom(e => e["AICEventType"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICEventType") : ""))
                           .ForMember(p => p.EventDate, src => src.MapFrom(e => SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICDate"])))))
                           .ForMember(p => p.EndDate, src => src.MapFrom(e => e["AICEventEndDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["AICEventEndDate"]))):(DateTime?)null))
                           .ForMember(p => p.Address, src => src.MapFrom(e => Convert.ToString(e["AICAddress"])))
                           .ForMember(p => p.Longitude, src => src.MapFrom(e => Convert.ToString(e["AICLongitude"])))
                           .ForMember(p => p.Latitude, src => src.MapFrom(e => Convert.ToString(e["AICLatitude"])))
                           .ForMember(p => p.Description, src => src.MapFrom(e => Convert.ToString(e["AICDesc"])))
                           .ForMember(p => p.EventImages, src => src.MapFrom(e => e["AICImageAlbum"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICImageAlbum") : ""))
                           .ForMember(p => p.EventVideos, src => src.MapFrom(e => e["AICVideoAlbum"] != null ? SharepointFieldHelpers.getLookupFieldValue(e, "AICVideoAlbum") : ""))
                           .ForMember(p => p.ShowInHomePage, src => src.MapFrom(e => Convert.ToBoolean(e["AICShowInHomePage"])))
                           .ForMember(p => p.ArchiveDate, src => src.MapFrom(e => e["ArchiveDate"] != null ? SharepointFieldHelpers.ConvertDateFromSharepoint(DateTime.Parse(Convert.ToString(e["ArchiveDate"]))) : (DateTime?) null));
        }
        private static void CreateCareersARMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CareersViewModel, CareersViewModelAR>();
        }
        private static void CreateCareersENMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CareersViewModel, CareersViewModelEN>();
        }
        private static void CreateInternShipsENMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<InternshipsViewModel, InternShipsViewModelEN>();
        }
        private static void CreateInternShipsARMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<InternshipsViewModel, InternShipsViewModelAR>();
        }
        private static void CreateCareersCacheARMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CareersViewModelAR, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.TitleAr))
                           .ForMember(p => p.Description, src => src.MapFrom(e => e.DescriptionAr))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.JobOverviewAr))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PublishingDate != null ? e.PublishingDate.Value : e.ModifiedDate));
        }
        private static void CreateCareersCacheENMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CareersViewModelEN, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Description, src => src.MapFrom(e => e.Description))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.JobOverview))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PublishingDate != null ? e.PublishingDate.Value : e.ModifiedDate));
        }
        private static void CreateInternshipsENCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<InternShipsViewModelEN, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.Title))
                           .ForMember(p => p.Description, src => src.MapFrom(e => e.Description))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.InternshipOverview))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PublishingDate != null ? e.PublishingDate.Value : e.ModifiedDate));
        }
        private static void CreateInternshipsARCacheMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<InternShipsViewModelAR, CacheViewModel>()
                           .ForMember(p => p.ListName, src => src.MapFrom(e => e.ListName))
                           .ForMember(p => p.Title, src => src.MapFrom(e => e.TitleAr))
                           .ForMember(p => p.Description, src => src.MapFrom(e => e.DescriptionAr))
                           .ForMember(p => p.Brief, src => src.MapFrom(e => e.InternshipOverviewAr))
                           .ForMember(p => p.Date, src => src.MapFrom(e => e.PublishingDate != null ? e.PublishingDate.Value : e.ModifiedDate));
        }
    }
}
