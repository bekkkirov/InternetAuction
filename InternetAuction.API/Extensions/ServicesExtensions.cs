using System;
using System.Text;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories;
using InternetAuction.Identity;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace InternetAuction.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILotCategoryRepository, LotCategoryRepository>();
            services.AddScoped<ILotRepository, LotRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(opt =>
                    {
                        opt.User.RequireUniqueEmail = true;
                        opt.Password.RequireUppercase = false;
                        opt.Password.RequireLowercase = false;
                        opt.Password.RequireNonAlphanumeric = false;
                        opt.Password.RequireDigit = false;
                        opt.Password.RequiredLength = 5;
                    })
                    .AddRoles<UserRole>()
                    .AddRoleManager<RoleManager<UserRole>>()
                    .AddSignInManager<SignInManager<User>>()
                    .AddRoleValidator<RoleValidator<UserRole>>()
                    .AddEntityFrameworkStores<IdentityContext>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, string key)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.RequireHttpsMetadata = false;
                        opt.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                            ValidateIssuerSigningKey = true
                        };
                    });

        }
    }
}