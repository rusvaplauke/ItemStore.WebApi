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

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task Delete(int id) 
        {
            if (await Get(id) == null) 
                throw new ArgumentNullException($"Item with id {id} not found.");
            
            if (await _itemRepository.Delete(id) == 0)
                throw new Exception($"Something went wrong; item not deleted");
        }

        public async Task<GetItemDto> Get(int id)
        {
            var response = await _itemRepository.Get(id);

            if (response == null)
                throw new ArgumentNullException($"Item with id {id} not found.");

            var result = new GetItemDto
            {
                Id = response.Id,
                Name = response.Name,
                Price = response.Price
            };

            return result;
        }

        public async Task<List<GetItemDto>> Get()
        {
            var response = await _itemRepository.Get();

            //if (response == null)
            //    throw new ArgumentNullException($"No items found."); // so is an empty DbSet not null?

            var result = response.Select(r => new GetItemDto
            {
                Id = r.Id,
                Name = r.Name,
                Price = r.Price
            }).ToList();

            return result;
        }

        public async Task<GetItemDto> Create(PostItemDto item)
        {
            var request = new ItemEntity
            {
                Name = item.Name,
                Price = item.Price
            };

            var response = await _itemRepository.Create(request);

            if (response == 0)
                throw new Exception($"Something went wrong; item not created"); 

            return await Get(response);
        }

        public async Task<GetItemDto> Edit(PutItemDto item)
        {
            if (await Get(item.Id) == null)
                throw new ArgumentNullException($"Item with id {item.Id} not found.");

            var request = new ItemEntity
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price
            };

            var response = await _itemRepository.Edit(request);

            if (response == 0)
                throw new ArgumentNullException($"Something went wrong; no items edited.");

            return await Get(response);
        }
    }
}
