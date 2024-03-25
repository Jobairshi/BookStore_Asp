namespace course.Repository.Irepository
{
    public interface IUnitOfWork
    {
        ICategoryRespository Category { get; }
        ICoverRepository Cover { get; }
        IProductRepository ProductType { get; }
        IComphanyRepository Comphany { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationRespository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }

        IOrderDetailRepository OrderDetail { get; }
        void Save();
    }
}
