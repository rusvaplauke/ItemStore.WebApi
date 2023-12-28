using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace ItemStore.WebApi.Clients
{
    public class JsonPlaceholderClient : IJsonPlaceholderClient
    {
        private IHttpClientFactory _httpClientFactory;

        public JsonPlaceholderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<UserEntity>> GetUsersAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
            var users = await response.Content.ReadAsAsync<List<UserEntity>>();

            return users;
        }

        public async Task<UserEntity> GetUserAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new UserNotFoundException(id);
            }

            return await response.Content.ReadAsAsync<UserEntity>();
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"https://jsonplaceholder.typicode.com/users/", user);

            return await response.Content.ReadAsAsync<UserEntity>();
        }
    }
}
