using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class ProductAttributeSetListConfiguration : IEntityTypeConfiguration<ProductAttributeSetList>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeSetList> builder)
        {
            builder.HasOne(d => d.ProductAttribute)
                .WithMany(p => p.ProductAttributeSetList)
                .HasForeignKey(d => d.ProductAttributeId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_ProductAttributeSetList_ProductAttribute");

            builder.HasOne(d => d.ProductAttributeSet)
                .WithMany(p => p.ProductAttributeSetList)
                .HasForeignKey(d => d.ProductAttributeSetId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_ProductAttributeSetList_ProductAttributeSet");
        }
    }
}
