using AIC.Data.BaseClasses;
using System;
using System.Collections.Generic;

#nullable disable

namespace AIC.Data.Model
{
    public partial class Subscriber : Entity<Guid>
    {
        //public long Id { get; set; }
        public string Email { get; set; }
        public DateTime? LastNewsLetterDate { get; set; }
        public DateTime? SubscriptionDate { get; set; }
    }
}
