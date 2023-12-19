using Dapper;
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
            string sql = "UPDATE items SET is_deleted = TRUE WHERE id = @id RETURNING id;"; 
            return _connection.QueryFirst<int>(sql, new {id = id });
        }

        public GetItemDto GetItem(int id)
        {
            string sql = "SELECT * FROM items WHERE id = @id AND is_deleted = FALSE;";

            var result = _connection.QuerySingleOrDefault<GetItemDto>(sql, new { id = id });

            if (result == null)
                throw new ArgumentException();
            else
                return result;
        }

        public IEnumerable<GetItemDto> GetItems()
        {
            return _connection.Query<GetItemDto>("SELECT * FROM items WHERE is_deleted = FALSE;");
        }

        public GetItemDto CreateItem(PostItemDto item)
        {
            string sql = "INSERT INTO items (name, price) VALUES (@Name, @Price) RETURNING id;";
            return GetItem(_connection.Execute(sql, item));
        }

        public GetItemDto EditItem(PutItemDto item)
        {
            string sql = "UPDATE items SET name = @Name, price = @Price WHERE id = @Id RETURNING id;";
            int itemId = _connection.QueryFirst<int>(sql, item);
            return GetItem(itemId);
        }

        public bool ItemExists(int id)
        {
            return _connection.QuerySingle<bool>("SELECT EXISTS (SELECT 1 from items WHERE id = @id AND is_deleted = FALSE);", new { id = id });
        }
    }
}
