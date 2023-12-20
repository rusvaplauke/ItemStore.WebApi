using Dapper;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.Entities;
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

        public int Delete(ItemEntity item) 
        {
            string sql = "UPDATE items SET is_deleted = TRUE WHERE id = @id;"; 

            return _connection.Execute(sql, new {id = item.id });
        }

        public ItemEntity? Get(ItemEntity item)
        {
            string sql = "SELECT * FROM items WHERE id = @id AND is_deleted = FALSE;";

            return _connection.QuerySingleOrDefault<ItemEntity>(sql, new { id = item.id });
        }

        public IEnumerable<ItemEntity> Get()
        {
            return _connection.Query<ItemEntity>("SELECT * FROM items WHERE is_deleted = FALSE;");
        }

        public int Create(ItemEntity item)
        {
            string sql = "INSERT INTO items (name, price) VALUES (@Name, @Price) RETURNING id;";

            return _connection.QuerySingleOrDefault<int>(sql, item);
        }

        public int Edit(ItemEntity item)
        {
            string sql = "UPDATE items SET name = @Name, price = @Price WHERE id = @Id RETURNING id;";

            return _connection.QuerySingleOrDefault<int>(sql, item);
        }
    }
}
