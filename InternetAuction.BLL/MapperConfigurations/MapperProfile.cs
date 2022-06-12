using AutoMapper;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.MapperConfigurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Image, ImageModel>().ReverseMap();
            CreateMap<LotCategory, LotCategoryModel>();
            CreateMap<Lot, LotModel>()
                .ForMember(d => d.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Bid, BidModel>();
            CreateMap<AppUser, AppUserModel>();
            CreateMap<RegisterModel, AppUser>();
        }
    }
}