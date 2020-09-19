using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class VendorStoreSliderConfiguration : IEntityTypeConfiguration<VendorStoreSlider>
    {
        public void Configure(EntityTypeBuilder<VendorStoreSlider> builder)
        {
            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.ImageUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.RedirectUrl)
                .HasMaxLength(500);

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorStoreSlider)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Vendor_VendorStoreSlider");
        }
    }
}