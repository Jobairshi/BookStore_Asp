using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class OrderHeaderReporsitory : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private database _db;
        public OrderHeaderReporsitory(database db):base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus)
        {
            var orderDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(orderDb != null)
            {
                orderDb.OrderStatus = orderStatus;
                if(paymentStatus != null)
                {
                    orderDb.PaymentStatus = paymentStatus;  
                }
            }
        }


        void IOrderHeaderRepository.UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

            orderDb.SessionId = sessionId;

            orderDb.PaymentIntentId = paymentIntentId;
        }
    }
}
