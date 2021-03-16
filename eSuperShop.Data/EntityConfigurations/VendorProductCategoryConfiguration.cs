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

            builder.Property(e => e.ImageFileName)
                .HasMaxLength(128);

            builder.Property(e => e.ChangedName)
                .HasMaxLength(400);

            builder.Property(e => e.ChangedImageFileName)
                .HasMaxLength(128);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorProductCategory)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorProductCategory_Vendor");
        }
    }
}