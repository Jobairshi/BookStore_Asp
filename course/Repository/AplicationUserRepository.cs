using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class AplicationUserRepository : Repository<ApplicationUser>, IApplicationRespository
    {
        private database _db;
        public AplicationUserRepository(database db):base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

     
       
    }
}
