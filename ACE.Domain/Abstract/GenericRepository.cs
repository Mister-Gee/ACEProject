using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Abstract
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
    }
}
