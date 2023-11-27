using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.ViewModels
{
    public class ListSubscribersViewModel
    {
        public IEnumerable<SubscriberVM> List { get; set; }
        public int TotalCount { get; set; }
    }
    public class SubscriberVM
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; } = DateTime.Now;
        public bool InvalidEmail { get; set; }
    }
}

