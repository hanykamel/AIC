using AIC.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIC.Data.BaseClasses
{
    public class Entity<T> :IDelete, IEntity<T> where T : struct
    {
        public Entity()
        {
            Created = DateTime.Now;
        }
        
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
