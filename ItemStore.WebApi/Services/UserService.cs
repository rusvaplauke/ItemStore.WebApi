using AutoMapper;
using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Services
{
    public class UserService
    {
        private readonly IJsonPlaceholderClient _client;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public UserService(IJsonPlaceholderClient client, IItemRepository itemRepository, IPurchaseRepository purchaseRepository, IMapper mapper) 
        {  
            _client = client;
            _purchaseRepository = purchaseRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUserDto>> GetAsync()
        {
            JsonPlaceholderResult<UserEntity> result = await _client.GetAsync();

            if (result.IsSuccessful == false)
                throw new JsonPlaceholderException(result.ErrorMessage);

            return (result.DataItems).Select(u => _mapper.Map<GetUserDto>(u)).ToList();
        }

        public async Task<GetUserDto> GetAsync(int id)
        {
            JsonPlaceholderResult<UserEntity> result = await _client.GetAsync(id);

            if (result.IsSuccessful ==  false)
                throw new JsonPlaceholderException(result.ErrorMessage);
            
            return _mapper.Map<GetUserDto>(result.DataItem);
        }

        public async Task<GetUserDto> CreateAsync(PostUserDto user)
        {
            JsonPlaceholderResult<UserEntity> result = await _client.CreateAsync(_mapper.Map<UserEntity>(user));

            if (result.IsSuccessful == false)
                throw new JsonPlaceholderException(result.ErrorMessage);

            return _mapper.Map<GetUserDto>(result.DataItem);
        }

        public async Task<PurchaseDto> BuyAsync(PurchaseDto purchase) // if user has already bought an item, don't do anything
        {
            var result = await _client.GetAsync(purchase.UserId);

            if (!result.IsSuccessful)
                throw new JsonPlaceholderException(result.ErrorMessage);

            if (await _itemRepository.GetAsync(purchase.ItemId) is null)
                throw new ItemNotFoundException(purchase.ItemId);

            if ((await _purchaseRepository.GetPurchasesByUserAsync(purchase)).Contains(purchase.ItemId))
                throw new PurchaseExistsException(purchase);

            return await _purchaseRepository.BuyAsync(purchase);
        }
    }
}
