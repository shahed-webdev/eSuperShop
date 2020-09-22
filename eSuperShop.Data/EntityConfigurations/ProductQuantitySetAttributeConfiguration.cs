using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductQuantitySetAttributeConfiguration : IEntityTypeConfiguration<ProductQuantitySetAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductQuantitySetAttribute> builder)
        {
            builder.HasOne(d => d.ProductAttributeValue)
                .WithMany(p => p.ProductQuantitySetAttribute)
                .HasForeignKey(d => d.ProductAttributeValueId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_ProductQuantitySetAttribute_ProductAttributeValue");

            builder.HasOne(d => d.ProductQuantitySet)
                .WithMany(p => p.ProductQuantitySetAttribute)
                .HasForeignKey(d => d.ProductQuantitySetId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductQuantitySetAttribute_ProductQuantitySet");
        }
    }
}
