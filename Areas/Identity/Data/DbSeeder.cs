using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Vidly.Areas.Identity.Data;

namespace Vidly.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync ( IServiceProvider service )
        {
            var userManager = service.GetService<UserManager<VidlyUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole("CanManageMovie"));
            


            var user = new VidlyUser
            {
                UserName = "admin@vidly.com",
                Email = "admin@vidly.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Adesh@2001");
                await userManager.AddToRoleAsync(user, "CanManageMovie");
            }

        }

    }
}
