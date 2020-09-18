using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eSuperShop.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedAdminData(this ModelBuilder builder)
        {
            var ADMIN_ID = "A0456563-F978-4135-B563-97F23EA02FDA";
            // any guid, but nothing is against to use the same one
            var ROLE_ID = "5A71C6C4-9488-4BCC-A680-445A34C6E721";
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ROLE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = ROLE_ID
                },
                new IdentityRole
                {
                    Id = "F73A5277-2535-48A4-A371-300508ADDD2F",
                    Name = "sub-admin",
                    NormalizedName = "SUB-ADMIN",
                    ConcurrencyStamp = "F73A5277-2535-48A4-A371-300508ADDD2F"
                },
                new IdentityRole
                {
                    Id = "D2FF0467-0FC2-4F98-9B6A-168079D1D9E7",
                    Name = UserType.Seller.ToString(),
                    NormalizedName = UserType.Seller.ToString().ToUpper(),
                    ConcurrencyStamp = "D2FF0467-0FC2-4F98-9B6A-168079D1D9E7"
                },
                new IdentityRole
                {
                    Id = "D4D44AB1-65A6-47C5-AD00-49834787E486",
                    Name = UserType.Customer.ToString(),
                    NormalizedName = UserType.Customer.ToString().ToUpper(),
                    ConcurrencyStamp = "D4D44AB1-65A6-47C5-AD00-49834787E486"
                });


            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEDch3arYEB9dCAudNdsYEpVX7ryywa8f3ZIJSVUmEThAI50pLh9RyEu7NjGJccpOog==",
                SecurityStamp = string.Empty,
                LockoutEnabled = true,
                ConcurrencyStamp = ADMIN_ID
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            builder.Entity<Registration>().HasData(new Registration
            {
                RegistrationId = 1,
                UserName = "Admin",
                Type = UserType.Admin,
                Name = "Admin"
            });
        }
    }
}
