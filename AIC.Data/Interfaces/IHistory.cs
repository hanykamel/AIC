using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.Interfaces
{
    public interface IHistory
    {
        public int? Version { get; set; }
        public bool? IsCurrent { get; set; }
        public string ActionBy { get; set; }
        public string Comments { get; set; }
    }
}
