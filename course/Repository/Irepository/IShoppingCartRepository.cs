using courseModels;

namespace course.Repository.Irepository
{
    public interface IShoppingCartRepository : IRepository<ShopingCart>
    {
        int IncreamentCount(ShopingCart shopingCart,int count);
        int DecrementCount(ShopingCart shopingCart, int count);
        
    }
}
