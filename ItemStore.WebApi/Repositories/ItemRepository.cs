using Dapper;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;
using System.Data;

namespace ItemStore.WebApi.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly IDbConnection _connection;

    public ItemRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> DeleteAsync(int id) 
    {
        string sql = "UPDATE items SET is_deleted = TRUE WHERE id = @id;"; 

        return await _connection.ExecuteAsync(sql, new {id = id });
    }

    public async Task<ItemEntity?> GetAsync(int id)
    {
        string sql = "SELECT * FROM items WHERE id = @id AND is_deleted = FALSE;";

        return await _connection.QuerySingleOrDefaultAsync<ItemEntity>(sql, new { id = id });
    }

    public async Task<List<ItemEntity>> GetAsync()
    {
        var result = await _connection.QueryAsync<ItemEntity>("SELECT * FROM items WHERE is_deleted = FALSE;");

        return result.ToList();
    }

    public async Task<int> CreateAsync(ItemEntity item)
    {
        string sql = "INSERT INTO items (name, price) VALUES (@Name, @Price) RETURNING id;";

        return await _connection.QuerySingleOrDefaultAsync<int>(sql, item);
    }

    public async Task<ItemEntity> EditAsync(ItemEntity item)
    {
        string sql = "UPDATE items SET name = @Name, price = @Price WHERE id = @Id RETURNING *;";

        return await _connection.QuerySingleOrDefaultAsync<ItemEntity>(sql,item);
    }
}
