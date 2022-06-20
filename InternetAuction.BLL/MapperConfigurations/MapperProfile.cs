using System.Linq;
using AutoMapper;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.MapperConfigurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Image, ImageModel>();
            CreateMap<LotCategory, LotCategoryModel>();
            CreateMap<Lot, LotModel>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Bid, BidModel>();
            CreateMap<AppUser, AppUserModel>().ReverseMap();
            CreateMap<RegisterModel, AppUser>();
            CreateMap<UserUpdateModel, AppUser>();
            CreateMap<LotCreateModel, Lot>();
            CreateMap<Lot, LotPreviewModel>()
                .ForMember(d => d.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault()))
                .ForMember(d => d.BidCount, opt => opt.MapFrom(src => src.Bids.Count))
                .ForMember(d => d.CurrentPrice, opt => opt.MapFrom(src => src.Bids.Count > 0 ? src.Bids.Max(b => b.BidValue) : src.InitialPrice));
        }
    }
}