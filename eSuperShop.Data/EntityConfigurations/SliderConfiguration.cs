using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.ImageUrl)
                .IsRequired()
                .HasColumnName("ImageURL")
                .HasMaxLength(255);

            builder.Property(e => e.ShownPlace)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.Sider)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sider_Registration");
        }
    }
}