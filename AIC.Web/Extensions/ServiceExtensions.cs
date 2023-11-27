using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using AIC.CrossCutting.MailService;
using AIC.Repository;
using AIC.Service.Implementation;
using AIC.Service.Interfaces;
using AIC.Web.Filters;
using AIC.SP.Middleware.Interfaces;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Services;
using AIC.CrossCutting.Helpers;
using AIC.CrossCutting.Interfaces;

namespace AIC.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddEntitiesScope(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Inject filters 
            services.AddScoped<AntiXssFilter>();
            //services.AddScoped<InternalApiFilter>();
            services.AddScoped<LanguageFilter>();
            services.AddScoped<ModelStateFilter>();

            //inject SP services
            services.AddScoped(typeof(IService<>), typeof(AIC.SP.Middleware.Services.Service<>));
            services.AddScoped(typeof(ISitemapService), typeof(SiteMapService));
            services.AddScoped(typeof(ICacheService), typeof(CacheService));
            services.AddScoped(typeof(ISearchService), typeof(SearchService));


            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
          
            services.AddScoped(typeof(INewsletterBusiness), typeof(NewsletterBusiness));

            services.AddScoped(typeof(ICustomCryptography), typeof(CustomCryptography));

         
            services.AddScoped(typeof(IUploadedDocumentsBusiness), typeof(UploadedDocumentsBusiness));

            services.AddTransient<IEmailService, EmailService>();
           
            services.AddScoped<ISendEmail, SendEmailBusiness>();

            services.AddScoped<IJoinUsBusiness, JoinUsBusiness>();
            services.AddScoped<IRequestLookupBusiness, RequestLookupBusiness>();

            services.AddScoped<ISynchronizationService, SynchronizationService>();

            services.AddScoped<IContactUsBusiness, ContactUsBusiness>();
            services.AddScoped<IVacancyBusiness, VacancyBusiness>();
            services.AddScoped<IInternShipsBusiness, InternShipsBusiness>();
            services.AddScoped<IRecaptchaBusiness, RecaptchaBusiness>();


            services.AddMemoryCache();

        }
    }
}
