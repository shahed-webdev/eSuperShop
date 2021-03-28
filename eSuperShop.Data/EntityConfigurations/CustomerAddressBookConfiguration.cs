using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class CustomerAddressBookConfiguration : IEntityTypeConfiguration<CustomerAddressBook>
    {
        public void Configure(EntityTypeBuilder<CustomerAddressBook> builder)
        {
            builder.Property(p => p.Address)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(p => p.Phone)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.AlternativePhone)
                .HasMaxLength(50);

            builder.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.CustomerAddressBook)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CustomerAddressBook_Customer");

            builder.HasOne(s => s.Area)
                .WithMany(c => c.CustomerAddress)
                .HasForeignKey(s => s.AreaId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_CustomerAddressBook_Area");

        }
    }
}