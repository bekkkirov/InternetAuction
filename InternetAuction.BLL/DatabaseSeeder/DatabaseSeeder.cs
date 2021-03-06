using Bogus;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus.Extensions;
using InternetAuction.BLL.Models.User;

namespace InternetAuction.BLL.DatabaseSeeder
{
    /// <summary>
    /// Represents a database seeder.
    /// </summary>
    public class DatabaseSeeder
    {
        /// <summary>
        /// Seeds the database.
        /// </summary>
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
                var users = new List<RegisterModel>()
                {
                    new RegisterModel()
                    {
                        UserName = "bekirov",
                        Email = "admin@mail.com",
                        FirstName = "Vladislav",
                        LastName = "Bekirov",
                        Password = "admin12345",
                    },

                    new RegisterModel()
                    {
                        UserName = "moderator",
                        Email = "moder@mail.com",
                        FirstName = "Moder",
                        LastName = "Moderov",
                        Password = "moder12345",
                    },
                };

                var fakeUsers = new Faker<RegisterModel>().RuleFor(u => u.UserName, f => (f.Internet.UserName() + f.IndexGlobal).ClampLength(1, 20))
                                                                          .RuleFor(u => u.FirstName, f => f.Person.FirstName.ClampLength(1, 30))
                                                                          .RuleFor(u => u.LastName, f => f.Person.LastName.ClampLength(1, 30))
                                                                          .RuleFor(u => u.Email, f => f.Internet.Email() + f.IndexGlobal)
                                                                          .RuleFor(u => u.Password, f => "password123")
                                                                          .Generate(10);

                users.AddRange(fakeUsers);

                //Registers administrator.
                await authorizationService.SignUpAsync(users[0]);
                await userManager.AddToRolesAsync(userManager.Users.First(), new[] { "Moderator", "Administrator" });

                //Registers moderator.
                await authorizationService.SignUpAsync(users[1]);
                await userManager.AddToRolesAsync(userManager.Users.OrderBy(u => u.Id).Last(), new[] { "Moderator" });

                //Registers ordinary users.
                for (int i = 2; i < users.Count; i++)
                {
                    await authorizationService.SignUpAsync(users[i]);
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

                for (int i = 0; i < images.Count; i++)
                {
                    registeredUsers[i].ProfileImage = images[i];
                }

                await unitOfWork.SaveChangesAsync();
            }

            if (!(await unitOfWork.LotCategoryRepository.GetAsync()).Any())
            {
                var categories = new List<LotCategory>()
                {
                    new LotCategory() { Name = "Electronics" },
                    new LotCategory() { Name = "Sports" },
                    new LotCategory() { Name = "Motors" },
                    new LotCategory() { Name = "Collectibles and Art" },
                };

                foreach (var category in categories)
                {
                    unitOfWork.LotCategoryRepository.Add(category);
                }

                await unitOfWork.SaveChangesAsync();
            }

            if (!(await unitOfWork.LotRepository.GetAsync()).Any())
            {
                var lotFaker = new Faker<Lot>().RuleFor(l => l.Name, f => f.Commerce.ProductName().ClampLength(1, 30))
                                               .RuleFor(l => l.Description, f => f.Commerce.ProductDescription().ClampLength(1, 250))
                                               .RuleFor(l => l.InitialPrice, f => f.Random.Decimal(10, 200))
                                               .RuleFor(l => l.Quantity, f => f.Random.Number(1, 3))
                                               .RuleFor(l => l.CategoryId, f => f.Random.Number(1, 4))
                                               .RuleFor(l => l.SaleEndTime, f => f.Date.Between(DateTime.Now, DateTime.Now.AddHours(72)))
                                               .RuleFor(l => l.SellerId, f => f.Random.Number(1, 5))
                                               .Generate(100);

                foreach (var lot in lotFaker)
                {
                    unitOfWork.LotRepository.Add(lot);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}