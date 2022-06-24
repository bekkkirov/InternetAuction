using FluentValidation.AspNetCore;
using InternetAuction.API.Extensions;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.MapperConfigurations;
using InternetAuction.BLL.Services;
using InternetAuction.BLL.Settings;
using InternetAuction.BLL.Validators;
using InternetAuction.DAL;
using InternetAuction.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using InternetAuction.BLL.Validators.User;

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
            services.AddControllers()
                    .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>());

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityDb")));
            services.AddDbContext<AuctionContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MainDb")));
            services.AddJwtAuthentication(Configuration["Jwt:Key"]);
            services.AddIdentity();
            services.AddRepositories();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddApplicationServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionMiddleware();

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
