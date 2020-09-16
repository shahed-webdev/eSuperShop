using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class AllSpecificationConfiguration : IEntityTypeConfiguration<AllSpecification>
    {

        public void Configure(EntityTypeBuilder<AllSpecification> builder)
        {
            builder.HasKey(e => e.SpecificationId);

            builder.Property(e => e.KeyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.AllowFiltering);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.AllSpecification)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllSpecification_Registration");

        }
    }


}
