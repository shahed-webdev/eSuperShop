using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class OrderShippingAddressConfiguration : IEntityTypeConfiguration<OrderShippingAddress>
    {
        public void Configure(EntityTypeBuilder<OrderShippingAddress> builder)
        {
            builder.Property(p => p.Address)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(p => p.Phone)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.AlternativePhone)
                .HasMaxLength(50);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(s => s.Order)
                .WithOne(c => c.OrderShippingAddress)
                .HasForeignKey<OrderShippingAddress>(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderShippingAddress_Order");


            builder.HasOne(s => s.Area)
                .WithMany(c => c.OrderShippingAddress)
                .HasForeignKey(s => s.AreaId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_OrderShippingAddress_Area");
        }
    }
}