using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.Property(e => e.RegistrationId).ValueGeneratedNever();

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.DateofBirth).HasMaxLength(50);

            builder.Property(e => e.Email).HasMaxLength(50);

            builder.Property(e => e.ImageUrl)
                .HasColumnName("ImageURL")
                .HasMaxLength(255);

            builder.Property(e => e.Name).HasMaxLength(128);

            builder.Property(e => e.Phone).HasMaxLength(50);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Validation)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        }
    }
}