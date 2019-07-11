using Microsoft.EntityDrameworkCore;
using ProductCatalog.Models;

namespace ProductCatalog.Data
{
    public class StoreDataContext : DbContext 
    {
        public DbSet<Product> Products { get; set; }
        public Dbset<Category> Categorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1444;Database=pdtctl;User ID=SA;Password=1q2w3e%&!");
        }

        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     builder.ApplyConfiguration(new ProductMap());
        //     builder.ApplyConfiguration(new CategoryMap());
        // }
    }
}