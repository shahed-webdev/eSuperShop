using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.NetAmount).
                HasComputedColumnSql("(([TotalAmount]-[Discount])+[ShippingCost])");

            builder.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.ShippingCost)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.OrderSn)
                .HasColumnName("OrderSN")
                .IsRequired();

            builder.Property(e => e.PaymentMethod)
                .HasMaxLength(50);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.ConfirmedOnUtc)
                .HasColumnType("datetime");

            builder.Property(e => e.DeliveredOnUtc)
                .HasColumnType("datetime");


            builder.HasOne(o => o.OrderShippingAddress)
                .WithMany(a => a.Order)
                .HasForeignKey(o => o.OrderShippingAddressId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Order_OrderShippingAddress");

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Order)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_Customer");
        }
    }
}