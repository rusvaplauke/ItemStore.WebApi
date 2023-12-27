using ItemStore.WebApi.Models.DTOs.UserDtos;

namespace ItemStore.WebApi.Clients
{
    public class JsonPlaceholderClient
    {
        private IHttpClientFactory _httpClientFactory;
        public JsonPlaceholderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<GetUserDto>> GetUsers()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
            var users = await response.Content.ReadAsAsync<List<GetUserDto>>();

            return users;
        }
        public async Task<GetUserDto> GetUser(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ArgumentNullException();
            }

            return await response.Content.ReadAsAsync<GetUserDto>();
        }
    }
}
