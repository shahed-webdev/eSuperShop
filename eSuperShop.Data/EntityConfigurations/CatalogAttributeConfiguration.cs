using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class CatalogAttributeConfiguration : IEntityTypeConfiguration<CatalogAttribute>
    {
        public void Configure(EntityTypeBuilder<CatalogAttribute> builder)
        {

            builder.Property(e => e.AssignedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.AssignedByRegistration)
                .WithMany(p => p.CatalogAttribute)
                .HasForeignKey(d => d.AssignedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogAttribute_Registration");

            builder.HasOne(d => d.Attribute)
                .WithMany(p => p.CatalogAttribute)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CatalogAttribute_AllAttribute");

            builder.HasOne(d => d.Catalog)
                .WithMany(p => p.CatalogAttribute)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CatalogAttribute_Catalog");

        }
    }
}
