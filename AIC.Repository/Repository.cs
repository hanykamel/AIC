using Microsoft.EntityFrameworkCore;
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
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
            where TEntity : class, IEntity<TId>,IDelete
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
        } 
        public void AddRange(IQueryable<TEntity> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.AddRange(entity);
        }


        public TEntity Get(TId id, bool overrideGlobalFilters = false)
        {
            return GetAll(overrideGlobalFilters).FirstOrDefault(c => c.Id.Equals(id));
        }
        public IQueryable<TEntity> GetAll(bool overrideGlobalFilters = false)
        {
            if (overrideGlobalFilters)
            {
                return _entities./*Where(o => o.IsDeleted != true).*/IgnoreQueryFilters();
            }
           // return _unitOfWork.CreateSet<TEntity>().Where(o => o.IsDeleted != true);
            return _entities;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool overrideGlobalFilters = false)
        {
            if (overrideGlobalFilters)
            {
                if (predicate != null)
                {
                    return GetAll().Where(predicate).IgnoreQueryFilters();
                }
                else
                {
                    throw new ArgumentNullException("The <predicate> paramter is required.");
                }
            }
            else
            {
                if (predicate != null)
                {
                    return GetAll().Where(predicate);
                }
                else
                {
                    throw new ArgumentNullException("The <predicate> paramter is required.");
                }
            }
        }
        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate, bool overrideGlobaleFilters = false)
        {
            if (predicate != null)
            {
                IQueryable<TEntity> query = GetAll(overrideGlobaleFilters)
                    .Where(predicate);
                return query.FirstOrDefault();
            }
            else
            {
                throw new ArgumentNullException("The <predicate> paramter is required.");
            }
        }

        public void LogicalDelete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.IsDeleted = true;
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Entry(entity).State = EntityState.Modified;
        } 
        
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Remove(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }           
            _entities.AddAsync(entity);
        }
    }
}
