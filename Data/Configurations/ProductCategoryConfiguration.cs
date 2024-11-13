using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursis.Data.Configurations
{
    internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {

            builder.HasKey(cc => new { cc.CategoryId, cc.ProductId });


            builder.HasOne(cc => cc.Category)
                   .WithMany(c => c.ProductCategories)
                   .HasForeignKey(cc => cc.CategoryId);


            builder.HasOne(cc => cc.Product)
                   .WithMany(c => c.ProductCategories)
                   .HasForeignKey(cc => cc.ProductId);

        }



    }
}
