using AutoMapper;
using ItemStore.WebApi.Clients;
using ItemStore.WebApi.Exceptions;
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
            JsonPlaceholderResult<UserEntity> result = await _client.GetUsersAsync();

            if (result.IsSuccessful == false)
                throw new JsonPlaceholderException(result.ErrorMessage);

            return (result.DataItems).Select(u => _mapper.Map<GetUserDto>(u)).ToList();
        }

        public async Task<GetUserDto> GetAsync(int id)
        {
            JsonPlaceholderResult<UserEntity> result = await _client.GetUserAsync(id);

            if (result.IsSuccessful ==  false)
                throw new JsonPlaceholderException(result.ErrorMessage);
            
            return _mapper.Map<GetUserDto>(result.DataItem);
        }

        public async Task<GetUserDto> CreateAsync(PostUserDto user)
        {
            JsonPlaceholderResult<UserEntity> result = await _client.CreateUserAsync(_mapper.Map<UserEntity>(user));

            if (result.IsSuccessful == false)
                throw new JsonPlaceholderException(result.ErrorMessage);

            return _mapper.Map<GetUserDto>(result.DataItem);
        }
    }
}
