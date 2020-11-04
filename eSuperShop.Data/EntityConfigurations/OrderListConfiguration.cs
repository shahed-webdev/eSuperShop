using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class OrderListConfiguration : IEntityTypeConfiguration<OrderList>
    {
        public void Configure(EntityTypeBuilder<OrderList> builder)
        {
            builder.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.CommissionPercentage)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([Quantity] * [UnitPrice])");

            builder.Property(e => e.CommissionAmount)
                .HasComputedColumnSql(" ((([Quantity] * [UnitPrice])*[CommissionPercentage])/100)");

            builder.HasOne(o => o.Order)
                .WithMany(o => o.OrderList)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OrderList_Order");

            builder.HasOne(o => o.Product)
                .WithMany(p => p.OrderList)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_OrderList_Product");

            builder.HasOne(o => o.ProductQuantitySet)
                .WithMany(p => p.OrderList)
                .HasForeignKey(o => o.ProductQuantitySetId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_OrderList_ProductQuantitySet");
        }
    }
}