using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class VendorFollowerConfiguration : IEntityTypeConfiguration<VendorFollower>
    {
        public void Configure(EntityTypeBuilder<VendorFollower> builder)
        {
            builder.Property(e => e.FollowedOnUtc)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.VendorFollower)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorFollower_Customer");

            builder.HasOne(d => d.Vendor)
                .WithMany(p => p.VendorFollower)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_VendorFollower_Vendor");

        }
    }
}
