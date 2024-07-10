using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestIgnatov.Models;

namespace TestIgnatov.Data
{
    public class ProductsDbContext : IdentityDbContext
    {
        public ProductsDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Users> Users { get; set;}
        public DbSet<Product> Products { get; set;}

    }
}