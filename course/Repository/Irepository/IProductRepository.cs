using courseModels;

namespace course.Repository.Irepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        void Save();
        
    }
}
