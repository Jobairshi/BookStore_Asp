using Microsoft.EntityFrameworkCore;

using courseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace courseAnotherPartDataAccess
{
    public class database:IdentityDbContext
    {
        public database(DbContextOptions<database>options):base(options)    
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }

        public DbSet<Product> ProductTypes { get; set; }

        public DbSet<ApplicationUser> AppUserType { get; set; }
        public DbSet<ComphanyPep> ComphanyPeps { get; set; }
        public DbSet<ShopingCart> shopingCarts{ get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetail { get; set; }


    }
}
