using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class CareersViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string JobType { get; set; }
        public string JobTypeAr { get; set; }
        public string ReportsTo { get; set; }
        public string ReportsToAr { get; set; }
        public string JobOverview { get; set; }
        public string JobOverviewAr { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
        public string JobQualifications { get; set; }
        public string JobQualificationsAr { get; set; }
        public string JobRequirements { get; set; }
        public string JobRequirementsAr { get; set; }
        public string Location { get; set; }
        public string LocationAr { get; set; }
        public DateTime VacancyExpiryDate { get; set; }
        public bool ShowInHomePage { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.CareersList; }
    }
    public class CareersListViewModel : IListItem<CareersViewModel>
    {
        public string PagingInfo { get; set; }
        public List<CareersViewModel> Items { get; set; }
    }
}
