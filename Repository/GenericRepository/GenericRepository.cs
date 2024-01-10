

using Microsoft.EntityFrameworkCore;
using Models.Common;
using Repository.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IDomainModel
    {
        private readonly Context _context;
        public GenericRepository(Context context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.Where(it => it.ID == id).SingleOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }
        public IEnumerable<T> GetIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate)
                .ToList();
        }

        public IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query
                    .ToList();
        }
    }
}
