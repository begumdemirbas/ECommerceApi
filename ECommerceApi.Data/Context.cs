using ECommerceApi.Data.EntityConfigurations;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate;
using ECommerceApi.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyConfigurations(modelBuilder);
            modelBuilder.ForSqlServerUseSequenceHiLo("DBSequenceHiLo");
        }

        private static void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProdutImages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}