using AutoMapper;
using ItemStore.WebApi.Models.DTOs.ShopDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Profiles;

public class ShopProfile : Profile
{
    public ShopProfile()
    {
        CreateMap<ShopEntity, GetShopDto>();
        CreateMap<ShopEntity, GetShopDto>().ReverseMap();

        CreateMap<ShopEntity, PutShopDto>();
        CreateMap<ShopEntity, PutShopDto>().ReverseMap();

        CreateMap<ShopEntity, PostShopDto>();
        CreateMap<ShopEntity, PostShopDto>().ReverseMap();
    }
}
