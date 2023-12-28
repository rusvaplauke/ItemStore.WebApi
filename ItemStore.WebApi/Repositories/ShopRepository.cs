using Dapper;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;
using System.Data;

namespace ItemStore.WebApi.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly IDbConnection _connection;

    public ShopRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> CreateAsync(ShopEntity shop)
    {
        string sql = "INSERT INTO shops (name, address) VALUES (@Name, @Address) RETURNING id;";

        return await _connection.QuerySingleOrDefaultAsync<int>(sql, shop);
    }

    public async Task<int> DeleteAsync(int id)
    {
        string sql = "UPDATE shops SET is_deleted = TRUE WHERE id = @id;";

        return await _connection.ExecuteAsync(sql, new { id = id });
    }

    public async Task<ShopEntity> EditAsync(ShopEntity shop)
    {
        string sql = "UPDATE shops SET name = @Name, address = @Address WHERE id = @Id;";

        return await _connection.QuerySingleOrDefaultAsync<ShopEntity>(sql, shop);
    }

    public async Task<List<ShopEntity>> GetAsync()
    {
        var result = await _connection.QueryAsync<ShopEntity>("SELECT * FROM shops WHERE is_deleted = FALSE;");

        return result.ToList();
    }

    public async Task<ShopEntity> GetAsync(int id)
    {
        string sql = "SELECT * FROM shops WHERE id = @id AND is_deleted = FALSE;";

        return await _connection.QuerySingleOrDefaultAsync<ShopEntity>(sql, new { id = id });
    }
}
