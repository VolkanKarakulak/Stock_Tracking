using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn(); // sql.server nuget paketinin yüklenmesi gerekiyor bu metodun kullanılabşlmesi için
            builder.Property(c =>c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Description).HasMaxLength(500);



           // builder.ToTable("Categories"); // tablo ismi vermek için kullanabilir
        }
    }
}
