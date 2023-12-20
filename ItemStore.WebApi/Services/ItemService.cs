using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ItemStore.WebApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public void Delete(int id) 
        {
            if (Get(id) == null) 
                throw new ArgumentNullException($"Item with id {id} not found.");
            
            var request = new ItemEntity
            {
                id = id
            };

            if (_itemRepository.Delete(request) == 0)
                throw new Exception($"Something went wrong; item not deleted");
        }

        public GetItemDto Get(int id)
        {
            var request = new ItemEntity
            {
                id = id
            };

            var response = _itemRepository.Get(request);

            if (response == null)
                throw new ArgumentNullException($"Item with id {id} not found.");

            var result = new GetItemDto
            {
                Id = response.id,
                Name = response.name,
                Price = response.price
            };

            return result;
        }

        public List<GetItemDto> Get()
        {
            var response = _itemRepository.Get();

            if (response == null)
                throw new ArgumentNullException($"No items found."); // so is an empty DbSet not null?

            var result = response.Select(r => new GetItemDto
            {
                Id = r.id,
                Name = r.name,
                Price = r.price
            }).ToList();

            return result;
        }

        public GetItemDto Create(PostItemDto item)
        {
            var request = new ItemEntity
            {
                name = item.Name,
                price = item.Price
            };

            var response = _itemRepository.Create(request);

            if (response == 0)
                throw new Exception($"Something went wrong; item not created"); // nepatinka, nes techniskai id irgi gb 0. gal tada reiktu id kurt ne int, o guid

            return Get(response);
        }

        public GetItemDto Edit(PutItemDto item)
        {
            if (Get(item.Id) == null)
                throw new ArgumentNullException($"Item with id {item.Id} not found.");

            var request = new ItemEntity
            {
                id = item.Id,
                name = item.Name,
                price = item.Price
            };

            var response = _itemRepository.Edit(request);

            if (response == 0)
                throw new ArgumentNullException($"Something went wrong; no items edited.");

            return Get(response);
        }
    }
}
