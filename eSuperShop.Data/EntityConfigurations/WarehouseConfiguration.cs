using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.Details)
                .HasMaxLength(500);

            builder.Property(e => e.Location)
                .HasMaxLength(255);

            builder.Property(e => e.Name)
                .HasMaxLength(128);

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.Warehouse)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_Registration");
        }
    }
}
