using System.Linq;
using AutoMapper;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Bid;
using InternetAuction.BLL.Models.Image;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.BLL.Models.User;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.MapperConfigurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //User
            CreateMap<AppUser, UserModel>();
            CreateMap<RegisterModel, AppUser>();
            CreateMap<UserUpdateModel, AppUser>();

            //Image
            CreateMap<Image, ImageModel>();

            //Lot
            CreateMap<LotCreateModel, Lot>();
            CreateMap<Lot, LotModel>()
                .ForMember(d => d.CurrentPrice, opt => opt.MapFrom(src => src.Bids.Count > 0 ? src.Bids.Max(b => b.BidValue) : src.InitialPrice));
            CreateMap<Lot, LotPreviewModel>()
                .ForMember(d => d.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault()))
                .ForMember(d => d.BidCount, opt => opt.MapFrom(src => src.Bids.Count))
                .ForMember(d => d.CurrentPrice, opt => opt.MapFrom(src => src.Bids.Count > 0 ? src.Bids.Max(b => b.BidValue) : src.InitialPrice));
            
            //LotCategory
            CreateMap<LotCategory, LotCategoryModel>();
            CreateMap<LotCategoryCreateModel, LotCategory>();
            
            //Bid
            CreateMap<Bid, BidModel>();
            CreateMap<BidCreateModel, Bid>();
        }
    }
}