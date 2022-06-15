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
                .ForMember(d => d.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Bid, BidModel>();
            CreateMap<AppUser, AppUserModel>().ReverseMap();
            CreateMap<RegisterModel, AppUser>();
            CreateMap<UserUpdateModel, AppUser>();
            CreateMap<LotCreateModel, Lot>();
            CreateMap<Lot, LotPreviewModel>()
                .ForMember(d => d.Image, opt => opt.MapFrom(src => src.Images.First()))
                .ForMember(d => d.BidCount, opt => opt.MapFrom(src => src.Bids.Count))
                .ForMember(d => d.CurrentPrice, opt => opt.MapFrom(src => src.Bids.Max(b => b.BidValue)));
        }
    }
}