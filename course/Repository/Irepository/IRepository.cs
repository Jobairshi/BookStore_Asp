using System.Linq.Expressions;

namespace course.Repository.Irepository
{
    public interface IRepository <T>where T :class//it means T is a class
    {
        T GetFirst(Expression<Func<T, bool>> filter);//find record a user
        IEnumerable<T> GetAll ();
        IEnumerable<T> GetAll(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = null
           );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
        void Add(T entity);
         void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
