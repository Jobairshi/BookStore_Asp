using courseModels;

namespace course.Repository.Irepository
{
    public interface ICoverRepository : IRepository<CoverType>
    {
        void Update(CoverType obj);
        void Save();
        
    }
}
