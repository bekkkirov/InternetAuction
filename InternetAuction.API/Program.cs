using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetAuction.BLL;
using InternetAuction.BLL.DatabaseSeeder;
using InternetAuction.BLL.Interfaces;
using InternetAuction.DAL.Interfaces;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace InternetAuction.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();
                var authorizationService = scope.ServiceProvider.GetRequiredService<IAuthorizationService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                await DatabaseSeeder.SeedDatabase(userManager, roleManager, authorizationService, unitOfWork);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
