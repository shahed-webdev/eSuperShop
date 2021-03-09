using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.Property(e => e.ImageFileName)
                .HasMaxLength(128);

            builder.Property(e => e.InsertedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.ProductAttribute)
                .WithMany(p => p.ProductAttributeValue)
                .HasForeignKey(d => d.ProductAttributeId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_ProductAttributeValue_ProductAttribute");
        }
    }
}