using bulky.DataAccess.Data;
using Bullky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bullky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> Set;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.Set = _db.Set<T>();
        }
        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> Query = Set;
            Query= Query.Where(predicate);
            return Query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> Query = Set;
            return Query.ToList();
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
        }
    }
    
}
