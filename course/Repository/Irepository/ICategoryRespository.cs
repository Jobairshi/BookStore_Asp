using courseModels;

namespace course.Repository.Irepository
{
    public interface ICategoryRespository : IRepository<Category>
    {
        void Update(Category obj);
        void Save();
        
    }
}
