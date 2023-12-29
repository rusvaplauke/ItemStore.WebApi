using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces;

public interface IJsonPlaceholderClient
{
    Task<JsonPlaceholderResult<UserEntity>> CreateAsync(UserEntity user);

    Task<JsonPlaceholderResult<UserEntity>> GetAsync(int id);

    Task<JsonPlaceholderResult<UserEntity>> GetAsync();
}