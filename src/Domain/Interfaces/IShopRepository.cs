using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces;

public interface IShopRepository
{
    Task<List<ShopEntity>> GetAsync();

    Task<ShopEntity> GetAsync(int id);

    Task<int> CreateAsync(ShopEntity shop);

    Task<ShopEntity> EditAsync(ShopEntity shop);

    Task<int> DeleteAsync(int id);
}
