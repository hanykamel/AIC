using AIC.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Repository
{
   public interface IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        IQueryable<TEntity> GetAll(bool overrideGlobalFilters = false); 
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool overrideGlobalFilters = false);
        TEntity Get(TId id, bool overrideGlobalFilters = false);
        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate,bool overrideGlobalFilters = false);
        void Add(TEntity entity);
        void AddRange(IQueryable<TEntity> entity);
        void Update(TEntity entity);
        void LogicalDelete(TEntity entity);
        void Delete(TEntity entity);
        Task AddAsync(TEntity entity);
    }
}
