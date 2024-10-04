using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.Contexts
{
    public class Stock_TrackingDbContext : DbContext
    {
        public Stock_TrackingDbContext(DbContextOptions<Stock_TrackingDbContext> options) : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
    }
}
