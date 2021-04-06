using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSuperShop.Data
{
    public class GeneralSettingConfiguration : IEntityTypeConfiguration<GeneralSetting>
    {
        public void Configure(EntityTypeBuilder<GeneralSetting> builder)
        {
            builder.HasData(new GeneralSetting
            {
                GeneralSettingId = 1,
                OrderQuantityLimit = 5
            });
        }
    }
}