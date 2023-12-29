using Dapper;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using System.Data;

namespace ItemStore.WebApi.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly IDbConnection _connection;

    public PurchaseRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<PurchaseDto> BuyAsync(PurchaseDto purchase)
    {
        string sql = "INSERT INTO purchases (userid, itemid) VALUES (@UserId, @ItemId) RETURNING *;";

        return await _connection.QuerySingleOrDefaultAsync<PurchaseDto>(sql, purchase);
    }

    public async Task<List<int>> GetPurchasesByUserAsync(PurchaseDto purchase)
    {
        string sql = "SELECT itemid FROM purchases WHERE userid = @UserId;";

        return (await _connection.QueryAsync<int>(sql, purchase)).ToList();
    }
}
