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

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.OrderShippingAddress)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderShippingAddress_Customer");
        }
    }
}