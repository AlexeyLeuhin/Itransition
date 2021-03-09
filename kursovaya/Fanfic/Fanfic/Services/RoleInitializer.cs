using Fanfic.Data;
using Microsoft.AspNetCore.Identity;
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

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
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