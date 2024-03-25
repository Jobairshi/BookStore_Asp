using course.Repository.Irepository;
using courseAnotherPartDataAccess;


namespace course.Repository
{
    public class UnitOfWorkCover : IUnitOfWorkCover
    {
       // public ICategoryRespository Category { get; private set; }
        private database _db;

        public UnitOfWorkCover(database db)
        {
            _db = db;
            CoverType = new CategoryRepositoryCover(_db);
         
        }
        public ICoverRepository CoverType { get; private set; }
       // public ICoverRepository CoverType { get; private set; }
       // ICoverRepository IUnitOfWork.CoverType => throw new NotImplementedException();

        public void Save()
        {
            _db.SaveChanges();
        }

        
    }
}
