using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class VendorProductCategoryListConfiguration : IEntityTypeConfiguration<VendorProductCategoryList>
    {
        public void Configure(EntityTypeBuilder<VendorProductCategoryList> builder)
        {
            builder.HasOne(d => d.Product)
                .WithMany(p => p.VendorProductCategoryList)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_VendorProductCategoryList_Product");

            builder.HasOne(d => d.VendorProductCategory)
                .WithMany(p => p.VendorProductCategoryList)
                .HasForeignKey(d => d.VendorProductCategoryId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorProductCategoryList_VendorProductCategory");
        }
    }
}