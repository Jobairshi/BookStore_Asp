using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private database _db;
        public OrderDetailsRepository(database db):base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderDetails obj)
        {
            _db.OrderDetail.Update(obj);
        }

       
    }
}
