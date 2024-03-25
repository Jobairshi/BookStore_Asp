using courseModels;

namespace course.Repository.Irepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStatus(int id,string orderDtatus,string?paymentStatus = null);
        void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
        void Save();
    }
}
