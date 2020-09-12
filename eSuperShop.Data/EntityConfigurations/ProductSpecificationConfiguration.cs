using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
    {
        public void Configure(EntityTypeBuilder<ProductSpecification> builder)
        {

            builder.Property(e => e.InsertedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductSpecification)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductSpecification_Product");

            builder.HasOne(d => d.Specification)
                .WithMany(p => p.ProductSpecification)
                .HasForeignKey(d => d.SpecificationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductSpecification_AllSpecification");

        }
    }
}
