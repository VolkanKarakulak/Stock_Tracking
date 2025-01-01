using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();          

            builder
                .HasOne(p => p.ProductStock)
                .WithOne(ps => ps.Product)
                .HasForeignKey<ProductStock>(ps => ps.ProductId);

            //builder.ToTable("Products");
        }
    }
}
