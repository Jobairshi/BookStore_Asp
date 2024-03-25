using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;

namespace course.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
       // public ICategoryRespository Category { get; private set; }
        private database _db;

        public UnitOfWork(database db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            ProductType = new ProductRepository(_db);
            Cover = new CategoryRepositoryCover(_db);
           Comphany = new ComphanyRepository (_db);
            ApplicationUser = new AplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderReporsitory(_db);
            OrderDetail = new OrderDetailsRepository(_db);
        }
        public ICategoryRespository Category { get; private set; }
        public IProductRepository ProductType { get; private set; }
        public ICoverRepository Cover { get; private set; }
        public IComphanyRepository Comphany { get; private set; }
        // public ICoverRepository CoverType { get; private set; }
        // ICoverRepository IUnitOfWork.CoverType => throw new NotImplementedException();
        public IShoppingCartRepository ShoppingCart { get; }
        public IApplicationRespository ApplicationUser { get; }
        public IOrderHeaderRepository OrderHeader { get; }

        public IOrderDetailRepository OrderDetail { get; }

        public void Save()
        {
            
            _db.SaveChanges();
        }
    }
}
