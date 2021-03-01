using AutoMapper;
using eSuperShop.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace eSuperShop.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
                {
                    config.Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequiredLength = 6,
                        RequiredUniqueChars = 0,
                        RequireLowercase = false,
                        RequireNonAlphanumeric = false,
                        RequireUppercase = false
                    };
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Account/Login";
            });

            // Dependency Injection
            services.AddDependencyInjection();


            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            //Mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //google login
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "612248071655-ejs1j0sagf8r1k0dn41j88uktl23tqet.apps.googleusercontent.com";
                googleOptions.ClientSecret = "3LqSP3xBZx2sHZ2hM479UWJ_";
            });

            //facebook login
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "422952768651477";
                facebookOptions.AppSecret = "a2f2c1b1fb31c2dfd57a9f7be373e760";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "Home",
                    "",
                    new { Controller = "Home", Action = "Index" }
                );

                endpoints.MapControllerRoute(
                    "Default",
                    "{controller}/{action}/{id?}"
                );

                endpoints.MapControllerRoute(
                    "Category",
                    "category/{slugUrl}",
                    new { Controller = "Category", Action = "Products" }
                );

                endpoints.MapControllerRoute(
                    "Product Details",
                    "item/{slugUrl}",
                    new { Controller = "Product", Action = "Item" }
                );

                endpoints.MapControllerRoute(
                    "Store",
                    "{slugUrl}",
                    new { Controller = "Store", Action = "Profile" }
                );
            });
        }
    }
}
