using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            if (_itemRepository.ItemExists(id))
            {
                return _itemRepository.DeleteItem(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public GetItemDto GetItem(int id)
        {
            if (_itemRepository.ItemExists(id))
            {
                return _itemRepository.GetItem(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public List<GetItemDto> GetItems()
        {
            return _itemRepository.GetItems().ToList();
        }

        public GetItemDto CreateItem(PostItemDto item)
        {
            return _itemRepository.CreateItem(item);
        }

        public GetItemDto EditItem(PutItemDto item)
        {
            if (_itemRepository.ItemExists(item.Id))
            {
                return _itemRepository.EditItem(item);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
