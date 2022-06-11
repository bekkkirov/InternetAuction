using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.API
{
    public class DatabaseSeeder
    {
        public static async Task SeedDatabase(UserManager<User> userManager, RoleManager<UserRole> roleManager, IAuthorizationService authorizationService)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                var roles = new List<UserRole>()
                {
                    new UserRole() {Name = "User"},
                    new UserRole() {Name = "Moderator"},
                    new UserRole() {Name = "Administrator"},
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!await userManager.Users.AnyAsync())
            {
                var userData = await File.ReadAllTextAsync("users.json");
                var users = JsonSerializer.Deserialize<List<RegisterModel>>(userData);

                await authorizationService.SignUpAsync(users[0]);
                await userManager.AddToRolesAsync(userManager.Users.First(), new[] {"User", "Moderator", "Administrator"});

                await authorizationService.SignUpAsync(users[1]);
                await userManager.AddToRolesAsync(userManager.Users.OrderBy(u => u.Id).Last(), new[] { "User", "Moderator" });

                for (int i = 2; i < users.Count; i++)
                {
                    await authorizationService.SignUpAsync(users[i]);
                }

                var identityUsers = await userManager.Users.ToListAsync();

                for (int i = 2; i < identityUsers.Count; i++)
                {
                    await userManager.AddToRoleAsync(identityUsers[i], "User");
                }
            }
        }
    }
}