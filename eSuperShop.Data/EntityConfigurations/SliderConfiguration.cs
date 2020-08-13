using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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
            builder.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.RedirectUrl)
                .HasColumnName("RedirectURL")
                .HasMaxLength(500);
            builder.Property(e => e.DisplayPlace)
                .IsRequired()
                .HasMaxLength(128)
                .HasConversion(c => c.ToString(), c => Enum.Parse<SliderDisplayPlace>(c));

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.Sider)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slider_Registration");
        }
    }
}