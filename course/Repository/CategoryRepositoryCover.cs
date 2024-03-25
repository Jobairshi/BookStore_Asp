using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class CategoryRepositoryCover : Repository<CoverType>, ICoverRepository
    {
        private database _db;
        public CategoryRepositoryCover(database db):base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(CoverType obj)
        {
            _db.Update(obj);
        }

       
    }
}
