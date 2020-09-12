using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace eSuperShop.Data
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {

            builder.Property(e => e.DisplayType)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(c => c.ToString(), c => Enum.Parse<ProductAttributeDisplay>(c));

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(255);

            builder.Property(e => e.InsertedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.Attribute)
                .WithMany(p => p.ProductAttribute)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_ProductAttribute_AllAttribute");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductAttribute)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductAttribute_Product");

        }
    }
}
