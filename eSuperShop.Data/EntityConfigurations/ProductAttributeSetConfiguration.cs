using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductAttributeSetConfiguration : IEntityTypeConfiguration<ProductAttributeSet>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeSet> builder)
        {
            builder.Property(e => e.InsertedOnUtc)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.PriceAdjustment)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductAttributeSet)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductAttributeSet_Product");
        }
    }
}
