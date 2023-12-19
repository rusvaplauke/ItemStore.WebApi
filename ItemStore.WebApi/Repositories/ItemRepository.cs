using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ItemStore.WebApi.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDbConnection _connection;

        public ItemRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public int DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public GetItemDto GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetItemDto> GetItems()
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
