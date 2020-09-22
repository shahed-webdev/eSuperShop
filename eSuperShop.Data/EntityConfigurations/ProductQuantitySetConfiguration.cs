using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductQuantitySetConfiguration : IEntityTypeConfiguration<ProductQuantitySet>
    {
        public void Configure(EntityTypeBuilder<ProductQuantitySet> builder)
        {
            builder.Property(e => e.InsertedOnUtc)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.PriceAdjustment)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductQuantitySet)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductQuantitySet_Product");
        }
    }
}
