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

            builder.Property(e => e.StoreSlugUrl)
                .HasMaxLength(255);

            builder.Property(e => e.StoreBannerFileName)
                .HasMaxLength(128);

            builder.Property(e => e.StoreLogoFileName)
                .HasMaxLength(128);

            builder.Property(e => e.StoreTagLine)
                .HasMaxLength(255);

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

            builder.Property(e => e.NId)
                .HasMaxLength(50);

            builder.Property(e => e.NIdImageBackFileName)
                .HasMaxLength(128);

            builder.Property(e => e.NIdImageFrontFileName)
                .HasMaxLength(128);

            builder.Property(e => e.TradeLicenseImageFileName)
                .HasMaxLength(128);
            builder.Property(e => e.StorePostcode)
                .HasMaxLength(50);


            builder.Property(e => e.BankAccountTitle)
                .HasMaxLength(128);
            builder.Property(e => e.BankAccountNumber)
                .HasMaxLength(128);
            builder.Property(e => e.BankName)
                .HasMaxLength(128);
            builder.Property(e => e.BranchName)
                .HasMaxLength(128);
            builder.Property(e => e.RoutingNumber)
                .HasMaxLength(128);
            builder.Property(e => e.ChequeImageFileName)
                .HasMaxLength(255);
            builder.Property(e => e.MobileBankingType)
                .HasMaxLength(50);
            builder.Property(e => e.BankAccountNumber)
                .HasMaxLength(50);


            builder.Property(e => e.WarehouseAddress)
                .HasMaxLength(128);
            builder.Property(e => e.WarehousePostcode)
                .HasMaxLength(50);
            builder.Property(e => e.WarehousePhone)
                .HasMaxLength(50);
            builder.Property(e => e.ReturnAddress)
                .HasMaxLength(128);
            builder.Property(e => e.ReturnPhone)
                .HasMaxLength(50);
            builder.Property(e => e.ReturnPostcode)
                .HasMaxLength(50);

            builder.Property(e => e.MobileBankingNumber)
                .HasMaxLength(50);

            builder.HasOne(d => d.StoreArea)
                .WithMany(p => p.VendorStore)
                .HasForeignKey(d => d.StoreAreaId)
                .HasConstraintName("FK_Area_VendorStore")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.WarehouseArea)
                .WithMany(p => p.VendorWarehouse)
                .HasForeignKey(d => d.WarehouseAreaId)
                .HasConstraintName("FK_Area_VendorWarehouse")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.ReturnArea)
                .WithMany(p => p.VendorReturn)
                .HasForeignKey(d => d.ReturnAreaId)
                .HasConstraintName("FK_Area_VendorReturnArea")
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(e => e.ChangedStoreLogoFileName)
                .HasMaxLength(128);
            builder.Property(e => e.ChangedStoreBannerFileName)
                .HasMaxLength(128);
            builder.Property(e => e.ChangedStoreTagLine)
                .HasMaxLength(255);
            builder.Property(e => e.IsChangedApproved)
                .HasDefaultValue(true);
        }
    }
}
