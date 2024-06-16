using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.Contexts
{
    public class Stock_TrackingDbContext : DbContext
    {
        public Stock_TrackingDbContext(DbContextOptions<Stock_TrackingDbContext> options) : base(options)
        {
        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

    }
}
