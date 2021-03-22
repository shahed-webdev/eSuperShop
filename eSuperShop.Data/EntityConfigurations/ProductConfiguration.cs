using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.AdminComment)
                .HasMaxLength(4000);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(400);
            builder.Property(e => e.SlugUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.OldPrice)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.ShortDescription)
                .HasMaxLength(4000);

            builder.Property(e => e.SpecialPrice)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.SpecialPriceEndDateTimeUtc)
                .HasColumnType("datetime");

            builder.Property(e => e.SpecialPriceStartDateTimeUtc)
                .HasColumnType("datetime");

            builder.Property(e => e.UpdatedOnUtc)
                .HasColumnType("datetime");

            builder.Property(e => e.DeleteReason)
                .HasMaxLength(512);

            builder.HasOne(d => d.Brand)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Product_AllBrand");

            builder.HasOne(d => d.Catalog)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Product_Catalog");

            builder.HasOne(d => d.Seo)
                .WithOne(p => p.Product)
                .HasForeignKey<Product>(d => d.SeoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_SEO");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Product_Vendor");

        }
    }

}
