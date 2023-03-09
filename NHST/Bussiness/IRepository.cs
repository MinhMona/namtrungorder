using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHST.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> getall();
        IEnumerable<T> where(Func<T, bool> predicate);
        T insert(T entity);
        T update(T entity);
    }
}
