using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.Property(e => e.CatalogName)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(255);

            builder.Property(e => e.SlugUrl)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.UpdatedOnUtc)
                .HasColumnType("datetime");

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.Catalog)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Catalog_Registration");

            builder.HasOne(e => e.Seo)
                .WithOne(s => s.Catalog)
                .HasForeignKey<Catalog>(e => e.SeoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Catalog_Seo");

            builder.HasOne(d => d.ParentCatalog)
                .WithMany(p => p.SubCatalog)
                .HasForeignKey(d => d.ParentCatalogId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Catalog_Catalog");
        }
    }
}