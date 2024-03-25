using course.Repository.Irepository;
using courseAnotherPartDataAccess;
using courseModels;
using System.Linq.Expressions;

namespace course.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private database _db;
        public ProductRepository(database db):base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
            var objdb = _db.Find<Product>(obj.Id);
            if (objdb != null)
            {
                objdb.Title = obj.Title;
                objdb.Price50 = obj.Price50;
                objdb.CoverTypeId = obj.CoverTypeId;
                objdb.ISBN = obj.ISBN;
                objdb.ListPrice = obj.ListPrice;
                objdb.Price100 = obj.Price100;
                objdb.Author = obj.Author;
                objdb.Description = obj.Description;
                objdb.CategoryId = obj.CategoryId;
                objdb.Price = obj.Price;
            }
            if (obj.ImageUrl != null)
            {
                objdb.ImageUrl = obj.ImageUrl;
            }

        }


    }
}
