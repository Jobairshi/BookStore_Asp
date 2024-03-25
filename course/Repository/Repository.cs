using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace course.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly database _db;
        internal DbSet<T> dbset;
        public Repository(database db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }
        
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public IEnumerable<T> GetAll() //getall the list of users
        {
            IQueryable<T> query = dbset;
            return query.ToList();
        }
        
        public T GetFirst(Expression<Func<T, bool>> filter) //to find the first user by name
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
            query.FirstOrDefault();
            return query.FirstOrDefault();
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }


            return query.FirstOrDefault();
        }


        public void Remove(T entity) //remove the user
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}
