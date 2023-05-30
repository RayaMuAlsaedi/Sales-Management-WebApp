using Microsoft.AspNetCore.Identity;
using SaleInvoicesApp.Models;

namespace SaleInvoicesApp.Data
{
    public class SalesAppSeeds
    {
        public static async Task SeedRolesAsync(UserManager<SalesStaff> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));//Create admin role in db
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Cashier.ToString()));//Create Cashier role in db
        }

        /// <summary>
        /// Add admin user by default
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task AddAdminUserAsync(UserManager<SalesStaff> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User, here admin user is created by default. admin can create more users (actors and directors)
            var defaultUser = new SalesStaff
            {
                UserName = "admin@domain.com",
                Email = "admin@domain.com",
                FirstName = "Rayan",
                LastName = "Rayan",
                PhoneNumber = "1234567",
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                }
            }
        }
    }
}
