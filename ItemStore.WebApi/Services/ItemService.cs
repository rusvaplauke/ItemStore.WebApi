using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public int DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public GetItemDto GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<GetItemDto> GetItems()
        {
            throw new NotImplementedException();
        }

        public GetItemDto PostItem(PostItemDto item)
        {
            throw new NotImplementedException();
        }

        public GetItemDto PutItem(PutItemDto item)
        {
            throw new NotImplementedException();
        }
    }
}
