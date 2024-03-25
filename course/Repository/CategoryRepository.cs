using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRespository
    {
        private database _db;
        public CategoryRepository(database db):base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }

       
    }
}
