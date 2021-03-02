using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class VendorCertificateConfiguration : IEntityTypeConfiguration<VendorCertificate>
    {
        public void Configure(EntityTypeBuilder<VendorCertificate> builder)
        {

            builder.HasOne(d => d.Vendor)
                .WithMany(s => s.VendorCertificate)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VendorCertificate_Vendor");
        }
    }
}