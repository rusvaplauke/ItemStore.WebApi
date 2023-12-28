using AutoMapper;
using ItemStore.WebApi.Models.DTOs.ItemDtos;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, PostUserDto>();
        CreateMap<UserEntity, PostUserDto>().ReverseMap();

        CreateMap<UserEntity, GetUserDto>();
        CreateMap<UserEntity, GetUserDto>().ReverseMap();
    }
}
