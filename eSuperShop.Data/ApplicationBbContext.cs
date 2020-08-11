using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eSuperShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<CatalogShownPlace> CatalogShownPlace { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Seo> Seo { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogConfiguration());
            builder.ApplyConfiguration(new CatalogShownPlaceConfiguration());
            builder.ApplyConfiguration(new RegistrationConfiguration());
            builder.ApplyConfiguration(new SeoConfiguration());
            builder.ApplyConfiguration(new SliderConfiguration());

            base.OnModelCreating(builder);
            builder.SeedAdminData();
        }
    }
}
