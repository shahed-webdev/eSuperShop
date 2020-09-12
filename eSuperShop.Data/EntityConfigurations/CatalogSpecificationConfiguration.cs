using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class CatalogSpecificationConfiguration : IEntityTypeConfiguration<CatalogSpecification>
    {
        public void Configure(EntityTypeBuilder<CatalogSpecification> builder)
        {

            builder.Property(e => e.AssignedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(d => d.AssignedByRegistration)
                .WithMany(p => p.CatalogSpecification)
                .HasForeignKey(d => d.AssignedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogSpecification_Registration");

            builder.HasOne(d => d.Catalog)
                .WithMany(p => p.CatalogSpecification)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CatalogSpecification_Catalog");

            builder.HasOne(d => d.Specification)
                .WithMany(p => p.CatalogSpecification)
                .HasForeignKey(d => d.SpecificationId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_CatalogSpecification_AllSpecification");

        }
    }
}
