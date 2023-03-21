using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace EquipmentManagement.Auth;

internal class UserInitializer
{    

    public static async Task Initialize(RoleManager<IdentityRole> roleManager,
                                        UserManager<IdentityUser> userManager,
                                        IConfiguration configuration)
    {
          

        if(await roleManager.FindByNameAsync(Roles.User) is null)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.User));
        }
        if (await roleManager.FindByNameAsync(Roles.Admin) is null)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }

        if ((await userManager.GetUsersInRoleAsync(Roles.Admin)).Count() == 0)
        {
            var adminUsername = configuration.GetRequiredSection("AdminUsername").Value;
            var adminPassword = configuration.GetRequiredSection("AdminPassword").Value;
            var adminUser = new IdentityUser(adminUsername);
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if(!result.Succeeded)
            {
                throw new Exception();//TODO: normal exception
            }
            await userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }
}
