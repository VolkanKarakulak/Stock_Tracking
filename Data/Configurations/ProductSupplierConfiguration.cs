using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Data.Configurations
{
	internal class ProductSupplierConfiguration : IEntityTypeConfiguration<ProductSupplier>
	{
		public void Configure(EntityTypeBuilder<ProductSupplier> builder)
		{
			builder
				.HasKey(ps => new { ps.SupplierId, ps.ProductId });

			builder
				.HasOne(ps => ps.Supplier)
				.WithMany(s => s.ProductSuppliers)
				.HasForeignKey(ps => ps.SupplierId);

			builder
				.HasOne(ps => ps.Product)
				.WithMany(p => p.ProductSuppliers)
				.HasForeignKey(ps => ps.ProductId);
		}

	}
}
