using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class OrderCartConfiguration: IEntityTypeConfiguration<OrderCart>
    {
        public void Configure(EntityTypeBuilder<OrderCart> builder)
        {
            builder.HasOne(e => e.Customer)
                .WithMany(c => c.OrderCart)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Customer_OrderCart");

            builder.HasOne(e => e.Product)                  
                .WithMany(c => c.OrderCart)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Product_OrderCart");

            builder.HasOne(e => e.ProductQuantitySet)
                .WithMany(c => c.OrderCart)
                .HasForeignKey(e => e.ProductQuantitySetId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductQuantitySet_OrderCart");
        }
    }
}