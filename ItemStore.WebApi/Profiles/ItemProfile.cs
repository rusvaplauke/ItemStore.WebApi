using AutoMapper;
using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Profiles;
public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<ItemEntity, GetItemDto>();
        CreateMap<ItemEntity, GetItemDto>().ReverseMap();

        CreateMap<ItemEntity, PostItemDto>();
        CreateMap<ItemEntity, PostItemDto>().ReverseMap();

        CreateMap<ItemEntity, PutItemDto>();
        CreateMap<ItemEntity, PutItemDto>().ReverseMap();
    }
}
