using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Maps;
using ProductCatalog.Models;

namespace ProductCatalog.Data
{
	public class StoreDataContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categorys { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=localhost\sqlexpress;Database=pdtctl;User ID=SA;Password=1q2w3e%&!");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new ProductMap());
			builder.ApplyConfiguration(new CategoryMap());
		}
	}
}