using courseModels;

namespace course.Repository.Irepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
        void Save();
        
    }
}
