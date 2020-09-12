using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {

            builder.Property(e => e.Review)
                .HasMaxLength(500);

            builder.Property(e => e.ReviewedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.ProductReview)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductReview_Customer");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductReview)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductReview_Product");

        }
    }
}
