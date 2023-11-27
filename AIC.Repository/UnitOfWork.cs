using Microsoft.EntityFrameworkCore;
using AIC.Data;
using AIC.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AIC.Data.Model;

namespace AIC.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        
        }

        #endregion

        #region IUnitOfWork Members     
        public async Task<int> SaveChangesAsync()
        {
            SetIEntityFields();
            
            return await _context.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            SetIEntityFields();
            return _context.SaveChanges();
        }

        #endregion

        #region Private Methods

        private void SetIEntityFields()
        {
            var now = DateTime.Now;

            _context.ChangeTracker.Entries<IEntity<int>>()
            .Where(e => e.State == EntityState.Added)
            .ToList().ForEach(e =>
            {
                e.Entity.Created = now;
            });

            _context.ChangeTracker.Entries<IEntity<Guid>>()
            .Where(e => e.State == EntityState.Added)
            .ToList().ForEach(e =>
                {
                    e.Entity.Created = now;
                });

            _context.ChangeTracker.Entries<IEntity<long>>()
              .Where(e => e.State == EntityState.Added)
              .ToList().ForEach(e =>
              {
                  e.Entity.Created = now;
              });

          
            _context.ChangeTracker.Entries<IEntity<int>>()
            .Where(e => e.State == EntityState.Modified)
            .ToList()
            .ForEach(e =>
            {
                e.Entity.Modified = now;
            });

            _context.ChangeTracker.Entries<IEntity<Guid>>()
              .Where(e => e.State == EntityState.Modified)
              .ToList()
              .ForEach(e =>
              {
                  e.Entity.Modified = now;
              });

            _context.ChangeTracker.Entries<IEntity<long>>()
            .Where(e => e.State == EntityState.Modified)
            .ToList()
            .ForEach(e =>
            {
                e.Entity.Modified = now;
            });

         
        }

      
        #endregion

    }
}
