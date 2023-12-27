using ItemStore.WebApi.Clients;
using ItemStore.WebApi.Models.DTOs.UserDtos;

namespace ItemStore.WebApi.Services
{
    public class UserService
    {
        private readonly JsonPlaceholderClient _client;
        public UserService(JsonPlaceholderClient client) 
        {  
            _client = client; 
        }
        public async Task<List<GetUserDto>> GetAsync()
        {
            return await _client.GetUsers();
        }
        public async Task<GetUserDto> GetAsync(int id)
        {
            return await _client.GetUser(id);
        }
    }
}
