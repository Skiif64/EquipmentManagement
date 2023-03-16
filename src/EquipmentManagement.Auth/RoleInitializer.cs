using Microsoft.AspNetCore.Identity;

namespace EquipmentManagement.Auth;

internal class RoleInitializer
{    

    public static async Task Initialize(RoleManager<IdentityRole> roleManager)
    {
        if(await roleManager.FindByNameAsync(Roles.User) is null)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.User));
        }
        if (await roleManager.FindByNameAsync(Roles.Admin) is null)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }
    }
}
