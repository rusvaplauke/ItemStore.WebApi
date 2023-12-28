using Dapper;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.Entities;
using System.Data;

namespace ItemStore.WebApi.Repositories;

public class ShopItemRepository : IShopItemRepository
{
    private readonly IDbConnection _connection;

    public ShopItemRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AssignToStoreAsync(ShopItemDto shopItem) 
    {
        string sql = "INSERT INTO shop_items (shop_id, item_id) VALUES (@ShopId, @ItemId);";

        await _connection.QuerySingleOrDefaultAsync(sql, shopItem);
    }

    public async Task ChangeStoreAsync(ShopItemDto shopItem)
    {
        string sql = "UPDATE shop_items SET shop_id = @ShopId WHERE item_id = @ItemId;";

        await _connection.QuerySingleOrDefaultAsync(sql, shopItem);
    }

    public async Task<int> GetShopAsync(int itemId)
    {
        string sql = "SELECT shop_id FROM shop_items WHERE item_id = @itemId";

        return await _connection.QuerySingleOrDefaultAsync<int>(sql, itemId);
    }
}
