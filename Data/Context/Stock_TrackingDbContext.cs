using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Data.Contexts
{
    public class Stock_TrackingDbContext : DbContext
    {
        public Stock_TrackingDbContext(DbContextOptions<Stock_TrackingDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<TaxSetting> TaxSettings { get; set; }
        public DbSet<Earning> Earnings { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductStock>()
                .Ignore(ps => ps.Description)
                .Ignore(ps => ps.IsDeleted)
                .Ignore(ps => ps.CreatedDate);

			modelBuilder.Entity<Order>()
		        .Property(o => o.Status)
		        .HasConversion<byte>(); // Enum'ı tinyint ile eşleştirir


			//modelBuilder.Entity<ProductStock>()
			//    .Property(p => p.UpdatedDate)
			//    .HasColumnName("UpdatedDate");

			//modelBuilder.Entity<ProductStock>()
			//    .Property(p => p.IsActive)
			//    .HasColumnName("IsActive");
			;
        }
    }
}
