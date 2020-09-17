using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace eSuperShop.Data
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {

            builder.Property(e => e.ApprovedOnUtc)
                .HasColumnType("datetime");

            builder.Property(e => e.AuthorizedPerson)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Balance)
                .HasColumnType("decimal(22, 2)")
                .HasComputedColumnSql("([GrossSale]-((([Discount]+[Refund])+[Commission])+[Withdraw]))");

            builder.Property(e => e.Commission)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.GrossSale)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.NetSale)
                .HasColumnType("decimal(20, 2)")
                .HasComputedColumnSql("([GrossSale]-([Discount]+[Refund]))");

            builder.Property(e => e.Refund)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.StoreAddress)
                .HasMaxLength(500);

            builder.Property(e => e.StoreName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.StoreTheme)
                .HasMaxLength(50)
                .HasConversion(c => c.ToString(), c => Enum.Parse<StoreTheme>(c));

            builder.Property(e => e.VerifiedPhone)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Withdraw)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Registration)
                .WithMany(p => p.Vendor)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK_Vendor_Registration")
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
