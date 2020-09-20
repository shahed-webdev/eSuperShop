using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class VendorProductCategoryConfiguration : IEntityTypeConfiguration<VendorProductCategory>
    {
        public void Configure(EntityTypeBuilder<VendorProductCategory> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(255);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");


            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorProductCategory)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorProductCategory_Vendor");
        }
    }
}