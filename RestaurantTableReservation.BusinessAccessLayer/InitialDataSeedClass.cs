using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RestaurantTableReservation.DataAccessLayer;
using RestaurantTableReservation.DataAccessLayer.Entity;

namespace RestaurantTableReservation.BusinessAccessLayer
{
    public class InitialDataSeedClass
    {
        public static async Task SeedData(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<User>>();
            var context = service.GetRequiredService<ApplicationDbContext>();

            var roles = new[] {"admin","customer"};

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@gmail.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new User
                {
                    UserName = "admin",
                    NormalizedUserName = "Admin",
                    Name = "admin",
                    Status = UserStatus.Approved,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now,
                    NormalizedEmail = adminEmail.ToUpper(),
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(admin, "admin"))
                    await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
