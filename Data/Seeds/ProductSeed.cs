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
                 Price = 3500,
                 Stock = 15,
                
             },
             new Product
             {
                 Id = 2,
                 Name = "Roland Bateri",               
                 Price = 5500,
                 Stock = 5,
                
             });
        }
    }
}
