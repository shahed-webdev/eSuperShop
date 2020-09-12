using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class VendorWarehouseConfiguration : IEntityTypeConfiguration<VendorWarehouse>
    {
        public void Configure(EntityTypeBuilder<VendorWarehouse> builder)
        {

            builder.Property(e => e.AssignedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorWarehouse)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendorWarehouse_Vendor");

            builder.HasOne(d => d.Warehouse)
                .WithMany(p => p.VendorWarehouse)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendorWarehouse_Warehouse");

            builder.HasOne(d => d.AssignedByRegistration)
                .WithMany(p => p.VendorWarehouse)
                .HasForeignKey(d => d.AssignedByRegistrationId)
                .HasConstraintName("FK_VendorWarehouse_Registration")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

    }
}
