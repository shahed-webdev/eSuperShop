using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class AllBrandConfiguration : IEntityTypeConfiguration<AllBrand>
    {
        public void Configure(EntityTypeBuilder<AllBrand> builder)
        {
            builder.HasKey(e => e.BrandId);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.LogoUrl)
                .HasMaxLength(1024);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");


            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.AllBrand)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllBrand_Registration");

        }
    }

}
