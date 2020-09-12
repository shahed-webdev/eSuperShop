using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class VendorReviewConfiguration : IEntityTypeConfiguration<VendorReview>
    {
        public void Configure(EntityTypeBuilder<VendorReview> builder)
        {
            builder.Property(e => e.Review)
                .HasMaxLength(500);

            builder.Property(e => e.ReviewedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.VendorReview)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorReview_Customer");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorReview)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorReview_Vendor");

        }
    }
}
