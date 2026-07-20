using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TimeMatt.Identity;
using TimeMatt.Options;

namespace TimeMatt.Data;

public static class IdentitySeeder
{
    public static readonly string[] Roles = { "Owner", "TeamMember", "Client" };

    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var predefinedUser = services.GetRequiredService<IOptions<PredefinedUserOptions>>().Value;

        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var owner = await userManager.FindByEmailAsync(predefinedUser.Email);
        if (owner is null)
        {
            owner = new ApplicationUser
            {
                UserName = predefinedUser.Email,
                Email = predefinedUser.Email,
                FullName = predefinedUser.Name,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(owner, predefinedUser.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    "Failed to seed the predefined owner account: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        if (!await userManager.IsInRoleAsync(owner, "Owner"))
        {
            await userManager.AddToRoleAsync(owner, "Owner");
        }
    }
}
