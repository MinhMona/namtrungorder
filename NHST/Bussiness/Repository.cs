using NHST.Interfaces;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHST.Bussiness
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly NHSTEntities _context;

        public Repository(NHSTEntities context)
        {
            _context = context;
        }

        protected void Save() => _context.SaveChanges();

        public T insert(T entity)
        {
            _context.Set<T>().Add(entity);
            Save();
            return entity;
        }

        public T update(T entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Save();
            return entity;
        }

        public T delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save();
            return entity;
        }

        public IEnumerable<T> where(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> getall()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

    }
}
