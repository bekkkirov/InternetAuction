using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using InternetAuction.API.Extensions;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.MapperConfigurations;
using InternetAuction.BLL.Services;
using InternetAuction.BLL.Validators;
using InternetAuction.DAL;
using InternetAuction.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InternetAuction.API", Version = "v1" });
            });

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityDb")));
            services.AddDbContext<AuctionContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MainDb")));
            services.AddJwtAuthentication(Configuration["Jwt:Key"]);
            services.AddIdentity();
            services.AddRepositories();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);


            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IImageService, ImageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InternetAuction.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(opt => opt.AllowAnyOrigin()
                                                 .AllowAnyHeader()
                                                 .AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
