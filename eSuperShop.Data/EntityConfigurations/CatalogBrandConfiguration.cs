using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {

            builder.Property(e => e.AssignedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.AssignedByRegistration)
                .WithMany(p => p.CatalogBrand)
                .HasForeignKey(d => d.AssignedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogBrand_Registration");

            builder.HasOne(d => d.Brand)
                .WithMany(p => p.CatalogBrand)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CatalogBrand_AllBrand");

            builder.HasOne(d => d.Catalog)
                .WithMany(p => p.CatalogBrand)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CatalogBrand_Catalog");

        }
    }
}
