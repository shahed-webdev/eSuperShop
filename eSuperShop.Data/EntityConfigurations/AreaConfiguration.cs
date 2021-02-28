using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.Property(r => r.AreaName)
                .HasMaxLength(128)
                .IsRequired();
            builder.HasOne(r => r.Region)
                .WithMany(r => r.Areas)
                .HasForeignKey(a => a.RegionId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Area_Region");

        }
    }
}