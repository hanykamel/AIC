using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.Interfaces
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
