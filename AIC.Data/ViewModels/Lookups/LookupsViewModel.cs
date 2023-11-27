using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIC.Data.ViewModels.Lookups
{
    public class LookupsViewModel
    {
        public int Id { get; set; }
        public string TitleEn { get; set; }
        [Required]
        public string TitleAr { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class LookupsViewModelList
    {
        public IEnumerable<LookupsViewModel> List { get; set; }
        public int TotalCount { get; set; }
    }

}
