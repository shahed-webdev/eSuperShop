using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class SeoConfiguration : IEntityTypeConfiguration<Seo>
    {
        public void Configure(EntityTypeBuilder<Seo> builder)
        {
            builder.ToTable("SEO");

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.MetaDescription).HasMaxLength(4000);

            builder.Property(e => e.MetaKeywords).HasMaxLength(400);

            builder.Property(e => e.MetaTitle).HasMaxLength(400);

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.Seo)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SEO_Registration");
        }
    }
}