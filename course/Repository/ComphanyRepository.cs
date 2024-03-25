using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class ComphanyRepository : Repository<ComphanyPep>, IComphanyRepository
    {
        private database _db;
        public ComphanyRepository(database db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ComphanyPep obj)
        {
             _db.ComphanyPeps.Update(obj);
        }


    }
}
