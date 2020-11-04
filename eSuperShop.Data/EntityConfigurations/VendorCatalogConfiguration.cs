using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    class VendorCatalogConfiguration : IEntityTypeConfiguration<VendorCatalog>
    {
        public void Configure(EntityTypeBuilder<VendorCatalog> builder)
        {
            builder.Property(e => e.CommissionPercentage)
                .HasColumnType("decimal(5, 2)");

            builder.Property(e => e.AssignedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.AssignedByRegistration)
                .WithMany(p => p.VendorCatalog)
                .HasForeignKey(d => d.AssignedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendorCatalog_Registration");

            builder.HasOne(d => d.Catalog)
                .WithMany(p => p.VendorCatalog)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorCatalog_Catalog");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorCatalog)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorCatalog_Vendor");
        }
    }
}