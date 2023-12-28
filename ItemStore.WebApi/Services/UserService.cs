using AutoMapper;
using ItemStore.WebApi.Clients;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Services
{
    public class UserService
    {
        private readonly IJsonPlaceholderClient _client;
        private readonly IMapper _mapper;

        public UserService(IJsonPlaceholderClient client, IMapper mapper) 
        {  
            _client = client;
            _mapper = mapper;
        }

        public async Task<List<GetUserDto>> GetAsync()
        {
            return (await _client.GetUsersAsync()).Select(u => _mapper.Map<GetUserDto>(u)).ToList();
        }

        public async Task<GetUserDto> GetAsync(int id)
        {
            return _mapper.Map<GetUserDto>(await _client.GetUserAsync(id));
        }

        public async Task<GetUserDto> CreateAsync(PostUserDto user)
        {
            return _mapper.Map<GetUserDto>(await _client.CreateUserAsync(_mapper.Map<UserEntity>(user)));
        }
    }
}
