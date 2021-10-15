using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ACE.Domain.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);

        void Dispose();

        void FindAll(Expression<Func<T, bool>> predicate);

        bool Save();
    }
}
