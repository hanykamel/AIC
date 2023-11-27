using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.SP.Middleware.SPViewModels
{
    public class InternShipsBaseViewModel
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string ProjectDepartment { get; set; }
        public string Location { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ListName { get; set; }

    }
}
