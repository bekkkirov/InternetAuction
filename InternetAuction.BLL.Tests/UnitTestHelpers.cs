using AutoMapper;
using InternetAuction.BLL.MapperConfigurations;

namespace InternetAuction.BLL.Tests
{
    public static class UnitTestHelpers
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });

            return config.CreateMapper();
        }
    }
}