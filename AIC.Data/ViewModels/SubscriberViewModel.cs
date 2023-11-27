using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIC.Data.ViewModels
{
    public class SubscriberViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.]+\.[a-zA-Z]{2,4}$")]
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; } = DateTime.Now;
        public bool InvalidEmail { get; set; }
    }
}
