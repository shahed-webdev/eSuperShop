using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class AllAttributeConfiguration : IEntityTypeConfiguration<AllAttribute>
    {
        public void Configure(EntityTypeBuilder<AllAttribute> builder)
        {
            builder.HasKey(e => e.AttributeId);

            builder.Property(e => e.KeyName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.AllowFiltering);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.AllAttribute)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllAttribute_Registration");
        }
    }
}
