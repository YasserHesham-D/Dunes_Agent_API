using Domain.Models.Accounts;
using Microsoft.AspNetCore.Identity;

namespace Presentation.ServiceExtensions
{
    public static class SeedRolesHelper
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new[]
            {
                RoleConstants.TourAgent,
                RoleConstants.DiscAgent,
                RoleConstants.OperationManager,
                RoleConstants.Admin
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        //public static async Task SeedAdminAsync(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        //{
        //    string adminEmail = "admin123@dunes.com";
        //    string adminUserName = "admin";
        //    string adminPassword = "Admin@123";
        //    string adminPhoneNumber = "1234567890";

        //    if (await userManager.FindByEmailAsync(adminEmail) == null)
        //    {
        //        var adminUser = new Employee
        //        {
        //            UserName = adminUserName,
        //            Email = adminEmail,
        //            PhoneNumber = adminPhoneNumber,
        //            PhoneNumberConfirmed = true,
        //            EmailConfirmed = true,
        //            FullName = "System Admin",
        //            JoinDate = DateTime.UtcNow
        //        };

        //        var result = await userManager.CreateAsync(adminUser, adminPassword);

        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
        //        }
        //    }
        //}


        //public static async Task SeedAdminAsync(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        //{
        //    string adminEmail = "admin123@dunes.com";
        //    string adminUserName = "admin";
        //    string adminPassword = "Admin@123";
        //    string adminPhoneNumber = "1234567890";

        //    // Check if user already exists
        //    var existingUser = await userManager.FindByEmailAsync(adminEmail);
        //    if (existingUser != null)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Yellow;
        //        Console.WriteLine("⚠️  Admin user already exists — skipping creation.");
        //        Console.ResetColor();
        //        return;
        //    }

        //    // Create new admin user
        //    var adminUser = new Employee
        //    {
        //        UserName = adminUserName,
        //        Email = adminEmail,
        //        PhoneNumber = adminPhoneNumber,
        //        PhoneNumberConfirmed = true,
        //        EmailConfirmed = true,
        //        FullName = "System Admin",
        //        JoinDate = DateTime.UtcNow
        //    };

        //    var result = await userManager.CreateAsync(adminUser, adminPassword);

        //    if (!result.Succeeded)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("❌ Failed to create admin user:");
        //        foreach (var error in result.Errors)
        //        {
        //            Console.WriteLine($"   - {error.Code}: {error.Description}");
        //        }
        //        Console.ResetColor();
        //        return;
        //    }

        //    // Assign to Admin role
        //    var roleExists = await roleManager.RoleExistsAsync(RoleConstants.Admin);
        //    if (!roleExists)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("❌ Admin role does not exist. Run SeedRolesAsync first.");
        //        Console.ResetColor();
        //        return;
        //    }

        //    var roleAddResult = await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
        //    if (!roleAddResult.Succeeded)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("❌ Failed to assign admin role:");
        //        foreach (var error in roleAddResult.Errors)
        //        {
        //            Console.WriteLine($"   - {error.Code}: {error.Description}");
        //        }
        //    }
        //    else
        //    {
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine("✅ Admin user created and added to Admin role successfully!");
        //    }

        //    Console.ResetColor();
        //}

    }
}
