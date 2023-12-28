using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ItemStore.WebApi.Clients
{
    public class JsonPlaceholderClient : IJsonPlaceholderClient
    {
        private IHttpClientFactory _httpClientFactory;

        public JsonPlaceholderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<JsonPlaceholderResult<UserEntity>> GetUsersAsync() 
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");

            if (response.IsSuccessStatusCode)
            {
                List<UserEntity> data = await response.Content.ReadAsAsync<List<UserEntity>>();

                return new JsonPlaceholderResult<UserEntity>
                {
                    DataItems = data,
                    IsSuccessful = true
                };
            }
            else
            {
                return new JsonPlaceholderResult<UserEntity>
                {
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }

        public async Task<JsonPlaceholderResult<UserEntity>> GetUserAsync(int id) 
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");

            if (response.IsSuccessStatusCode)
            {
                UserEntity data = await response.Content.ReadAsAsync<UserEntity>();

                return new JsonPlaceholderResult<UserEntity>
                {
                    DataItem = data,
                    IsSuccessful = true
                };
            }
            else
            {
                return new JsonPlaceholderResult<UserEntity>
                {
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }

        public async Task<JsonPlaceholderResult<UserEntity>> CreateUserAsync(UserEntity user) 
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"https://jsonplaceholder.typicode.com/users/", user);

            if (response.IsSuccessStatusCode)
            {
                UserEntity data = await response.Content.ReadAsAsync<UserEntity>();

                return new JsonPlaceholderResult<UserEntity>
                {
                    DataItem = data,
                    IsSuccessful = true
                };
            }
            else
            {
                return new JsonPlaceholderResult<UserEntity>
                {
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };

            }
        }
    }
}
