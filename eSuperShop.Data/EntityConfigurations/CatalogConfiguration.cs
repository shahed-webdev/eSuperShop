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

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.ImageUrl)
                .HasColumnName("ImageURL")
                .HasMaxLength(255);

            builder.Property(e => e.SlugUrl)
                .IsRequired()
                .HasColumnName("SlugURL")
                .HasMaxLength(128);

            builder.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.Catalog)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Catalog_Registration");

            // builder.HasOne(e => e.Seo).WithOne().HasForeignKey<Seo>(e => e.SeoId);
        }
    }
}