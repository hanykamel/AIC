using AIC.CrossCutting.Interfaces.SPInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.SP.Middleware.SPViewModels
{
    public class InternshipsViewModel : IContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string ProjectDepartment { get; set; }
        public string ProjectDepartmentAr { get; set; }
        public string InternshipOverview { get; set; }
        public string InternshipOverviewAr { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
        public string InternshipQualifications { get; set; }
        public string InternshipQualificationsAr { get; set; }
        public string InternshipRequirements { get; set; }
        public string InternshipRequirementsAr { get; set; }
        public string Location { get; set; }
        public string LocationAr { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string WebUrl { get => ""; }

        public string ListName { get => ListsNames.Internships; }
    }

    public class InternshipsListViewModel : IListItem<InternshipsViewModel>
    {
        public string PagingInfo { get; set; }
        public List<InternshipsViewModel> Items { get; set; }
    }
}
