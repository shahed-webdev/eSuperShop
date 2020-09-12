using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductBlobConfiguration : IEntityTypeConfiguration<ProductBlob>
    {
        public void Configure(EntityTypeBuilder<ProductBlob> builder)
        {

            builder.Property(e => e.BlobUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.ProductBlob)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductBlob_Product");

        }
    }
}
