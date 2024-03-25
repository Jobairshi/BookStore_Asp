using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class ShoppingCartRepository : Repository<ShopingCart>, IShoppingCartRepository
    {
        private database _db;
        public ShoppingCartRepository(database db):base(db)
        {
            _db = db;
        }

        public int DecrementCount(ShopingCart shopingCart, int count)
        {
            shopingCart.count -= count;
            return shopingCart.count;
        }

        public int IncreamentCount(ShopingCart shopingCart, int count)
        {
            shopingCart.count += count;
            return shopingCart.count;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
      
       
    }
}
