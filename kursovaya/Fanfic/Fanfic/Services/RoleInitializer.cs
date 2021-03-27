using Fanfic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Fanfic.Services
{
    public class RoleInitializer
    {

        public enum RoleType
        {
            USER,
            ADMIN
        }

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            string adminEmail = config["Admin-Default-Email"];
            string password = config["Admin-Default-Password"];
            if (await roleManager.FindByNameAsync(nameof(RoleType.ADMIN)) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(nameof(RoleType.ADMIN)));
            }
            if (await roleManager.FindByNameAsync(nameof(RoleType.USER)) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(nameof(RoleType.USER)));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, nameof(RoleType.ADMIN));
                }
            }
        }
    }
}