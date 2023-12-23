using AutoMapper;
using ItemStore.WebApi.Exceptions;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ItemStore.WebApi.Services
{
    public class ItemService 
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task Delete(int id) 
        {
            if (await _itemRepository.Get(id) is null) 
                throw new ItemNotFoundException(id); 
        }

        public async Task<GetItemDto> Get(int id)
        {
            var response = await _itemRepository.Get(id);

            if (response == null)
                throw new ItemNotFoundException(id);

            return _mapper.Map<GetItemDto>(response);
        }

        public async Task<List<GetItemDto>> Get()
        {
            return (await _itemRepository.Get()).Select(r => _mapper.Map<GetItemDto>(r)).ToList();
        }

        public async Task<GetItemDto> Create(PostItemDto item)
        {
            return await Get(await _itemRepository.Create(_mapper.Map<ItemEntity>(item)));
        }

        public async Task<GetItemDto> Edit(PutItemDto item)
        {
            if (await _itemRepository.Get(item.Id) is null)
                throw new ItemNotFoundException(item.Id);

            return await Get(await _itemRepository.Edit(_mapper.Map<ItemEntity>(item))); 
            // is this clear or would it be easier to read in separate lines?
        }
    }
}
