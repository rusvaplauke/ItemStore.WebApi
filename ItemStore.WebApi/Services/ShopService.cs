using AutoMapper;
using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.ShopDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Services;

public class ShopService
{
    private readonly IShopRepository _shopRepository;
    private readonly IMapper _mapper;
    public ShopService(IShopRepository shopRepository, IMapper mapper)
    {
        _shopRepository = shopRepository;
        _mapper = mapper;
    }

    public async Task DeleteAsync(int id)
    {
        if (await _shopRepository.GetAsync(id) is null)
            throw new ShopNotFoundException(id);

        await _shopRepository.DeleteAsync(id);
    }

    public async Task<GetShopDto> GetAsync(int id)
    {
        var response = await _shopRepository.GetAsync(id);

        if (response == null)
            throw new ShopNotFoundException(id);

        return _mapper.Map<GetShopDto>(response);
    }

    public async Task<List<GetShopDto>> GetAsync()
    {
        return (await _shopRepository.GetAsync()).Select(r => _mapper.Map<GetShopDto>(r)).ToList();
    }

    public async Task<GetShopDto> CreateAsync(PostShopDto shop)
    {
        return await GetAsync(await _shopRepository.CreateAsync(_mapper.Map<ShopEntity>(shop)));
    }

    public async Task<GetShopDto> EditAsync(PutShopDto shop)
    {
        if (await _shopRepository.GetAsync(shop.Id) is null)
            throw new ShopNotFoundException(shop.Id);

        ShopEntity result = await _shopRepository.EditAsync(_mapper.Map<ShopEntity>(shop));

        return _mapper.Map<GetShopDto>(result);
    }
}
