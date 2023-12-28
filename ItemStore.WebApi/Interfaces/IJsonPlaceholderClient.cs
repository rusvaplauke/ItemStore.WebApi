using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces
{
    public interface IJsonPlaceholderClient
    {
        Task<JsonPlaceholderResult<UserEntity>> CreateUserAsync(UserEntity user);
        Task<JsonPlaceholderResult<UserEntity>> GetUserAsync(int id);
        Task<JsonPlaceholderResult<UserEntity>> GetUsersAsync();
    }
}