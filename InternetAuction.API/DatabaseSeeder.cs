using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.API
{
    public class DatabaseSeeder
    {
        public static async Task SeedDatabase(UserManager<User> userManager, RoleManager<UserRole> roleManager,
            IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
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
                //Gets user data from json file.
                var userData = await File.ReadAllTextAsync("users.json");
                var users = JsonSerializer.Deserialize<List<RegisterModel>>(userData);

                //Registers administrator.
                await authorizationService.SignUpAsync(users[0]);
                await userManager.AddToRolesAsync(userManager.Users.First(), new[] {"User", "Moderator", "Administrator"});

                //Registers moderator.
                await authorizationService.SignUpAsync(users[1]);
                await userManager.AddToRolesAsync(userManager.Users.OrderBy(u => u.Id).Last(), new[] { "User", "Moderator" });

                //Registers ordinary users.
                for (int i = 2; i < users.Count; i++)
                {
                    await authorizationService.SignUpAsync(users[i]);
                }

                //Adds roles to ordinary users
                var identityUsers = await userManager.Users.ToListAsync();

                for (int i = 2; i < identityUsers.Count; i++)
                {
                    await userManager.AddToRoleAsync(identityUsers[i], "User");
                }

                //List of profile images.
                var images = new List<Image>()
                {
                    new Image()
                    {
                        Url =
                            "https://res.cloudinary.com/dfmf6kbfk/image/upload/v1655122653/InternetAuction/1_tngyrd.jpg",
                        PublicId = "InternetAuction/1_tngyrd.jpg",
                    },

                    new Image()
                    {
                        Url =
                            "https://res.cloudinary.com/dfmf6kbfk/image/upload/v1655122654/InternetAuction/2_icyarf.jpg",
                        PublicId = "InternetAuction/2_icyarf.jpg",
                    },

                    new Image()
                    {
                        Url =
                            "https://res.cloudinary.com/dfmf6kbfk/image/upload/v1655122653/InternetAuction/3_aytnga.jpg",
                        PublicId = "InternetAuction/3_aytnga.jpg",
                    },

                    new Image()
                    {
                        Url =
                            "https://res.cloudinary.com/dfmf6kbfk/image/upload/v1655122653/InternetAuction/4_bnygbl.jpg",
                        PublicId = "InternetAuction/4_bnygbl.jpg",
                    },

                    new Image()
                    {
                        Url =
                            "https://res.cloudinary.com/dfmf6kbfk/image/upload/v1655122653/InternetAuction/5_mzajto.jpg",
                        PublicId = "InternetAuction/5_mzajto.jpg",
                    }
                };

                var registeredUsers = (await unitOfWork.UserRepository.GetAllWithDetailsAsync()).ToList();

                for(int i = 0; i < images.Count; i++)
                {
                    registeredUsers[i].ProfileImage = images[i];
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}