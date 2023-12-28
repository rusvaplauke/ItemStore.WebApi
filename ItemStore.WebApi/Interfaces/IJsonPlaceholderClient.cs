using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces
{
    public interface IJsonPlaceholderClient
    {
        Task<UserEntity> CreateUserAsync(UserEntity user);
        Task<UserEntity> GetUserAsync(int id);
        Task<List<UserEntity>> GetUsersAsync();
    }
}