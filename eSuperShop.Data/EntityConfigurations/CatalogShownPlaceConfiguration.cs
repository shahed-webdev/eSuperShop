using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace eSuperShop.Data
{
    public class CatalogShownPlaceConfiguration : IEntityTypeConfiguration<CatalogShownPlace>
    {
        public void Configure(EntityTypeBuilder<CatalogShownPlace> builder)
        {
            builder.Property(e => e.CatalogShownPlaceId);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.ShownPlace)
                .IsRequired()
                .HasMaxLength(128)
                .HasConversion(c => c.ToString(), c => Enum.Parse<CatalogDisplayPlace>(c));

            builder.HasOne(d => d.Catalog)
                .WithMany(p => p.CatalogShownPlace)
                .HasForeignKey(d => d.CatalogId)
                .HasConstraintName("FK_CatalogShownPlace_Catalog");

            builder.HasOne(d => d.CreatedByRegistration)
                .WithMany(p => p.CatalogShownPlace)
                .HasForeignKey(d => d.CreatedByRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogShownPlace_Registration");
        }
    }
}