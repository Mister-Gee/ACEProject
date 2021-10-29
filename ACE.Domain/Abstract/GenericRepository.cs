using ACE.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Abstract
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbContext entities;

        public GenericRepository(IDbContext ent)
        {
            entities = ent;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = entities.Set<T>();
            return query;
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {

            entities.Set<T>().Add(entity);

        }

        public void Delete(T entity)
        {
            entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual bool Save(string userName, string IP)
        {
            return entities.SaveChanges(userName, IP) > 0;
        }

        public virtual bool SaveAsync(string userName, string IP)
        {
            return entities.SaveChangesAsync(userName, IP).Result > 0;
        }

        public void Dispose()
        {
            // Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
