using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
    }
}