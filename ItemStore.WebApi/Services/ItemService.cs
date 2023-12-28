using AutoMapper;
using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.ItemDtos;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Services;

public class ItemService 
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task DeleteAsync(int id) 
    {
        if (await _itemRepository.GetAsync(id) is null) 
            throw new ItemNotFoundException(id); 

        await _itemRepository.DeleteAsync(id);
    }

    public async Task<GetItemDto> GetAsync(int id)
    {
        var response = await _itemRepository.GetAsync(id);

        if (response == null)
            throw new ItemNotFoundException(id);

        return _mapper.Map<GetItemDto>(response);
    }

    public async Task<List<GetItemDto>> GetAsync()
    {
        return (await _itemRepository.GetAsync()).Select(r => _mapper.Map<GetItemDto>(r)).ToList();
    }

    public async Task<GetItemDto> CreateAsync(PostItemDto item)
    {
        return await GetAsync(await _itemRepository.CreateAsync(_mapper.Map<ItemEntity>(item)));
    }

    public async Task<GetItemDto> EditAsync(PutItemDto item) 
    {
        if (await _itemRepository.GetAsync(item.Id) is null)
            throw new ItemNotFoundException(item.Id);

        ItemEntity result = await _itemRepository.EditAsync(_mapper.Map<ItemEntity>(item)); 

        return _mapper.Map<GetItemDto>(result); 
    }
}
