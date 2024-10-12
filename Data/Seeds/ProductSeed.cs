using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Data.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.HasData(
             new Product
             {
                 Id = 1,
                 Name = "Yamaha Piyano",
                 CategoryId = 1,
                 Price = 3500,
                 Stock = 15,
                
             },
             new Product
             {
                 Id = 2,
                 Name = "Roland Bateri",
                 CategoryId = 3,
                 Price = 5500,
                 Stock = 5,
                
             });
        }
    }
}
