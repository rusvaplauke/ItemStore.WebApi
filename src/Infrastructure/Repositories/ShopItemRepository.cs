using Dapper;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using System.Data;

namespace ItemStore.WebApi.Repositories;

public class ShopItemRepository : IShopItemRepository
{
    private readonly IDbConnection _connection;

    public ShopItemRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<ShopItemDto> AssignToShopAsync(ShopItemDto shopItem) 
    {
        string sql = "INSERT INTO shop_items (shopid, itemid) VALUES (@ShopId, @ItemId) RETURNING *;";

        return await _connection.QuerySingleOrDefaultAsync<ShopItemDto>(sql, shopItem);
    }

    public async Task<ShopItemDto> ChangeShopAsync(ShopItemDto shopItem)
    {
        string sql = "UPDATE shop_items SET shopid = @ShopId WHERE itemid = @ItemId RETURNING *;";

        return await _connection.QuerySingleOrDefaultAsync<ShopItemDto>(sql, shopItem);
    }

    public async Task<int> GetShopAsync(int itemId)
    {
        string sql = "SELECT shopid FROM shop_items WHERE itemid = @itemId;";

        return await _connection.QuerySingleOrDefaultAsync<int>(sql, new {itemId = itemId});
    }

    public async Task UnassignFromDeletedShopAsync(int shopId)
    {
        string sql = "DELETE FROM shop_items WHERE shopid = @shopId;";

        await _connection.ExecuteAsync(sql, new {shopId = shopId});
    }

    public async Task UnassignDeletedItemAsync(int itemId)
    {
        string sql = "DELETE FROM shop_items WHERE itemid = @itemId;";

        await _connection.ExecuteAsync(sql, new {itemId = itemId});
    }
}
