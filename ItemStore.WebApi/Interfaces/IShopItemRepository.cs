using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces;

public interface IShopItemRepository
{
    Task<ShopItemDto> AssignToShopAsync(ShopItemDto shopItem);

    Task<int> GetShopAsync(int itemId);

    Task<ShopItemDto> ChangeShopAsync(ShopItemDto shopItem);

    Task UnassignDeletedItemAsync(int itemId);

    Task UnassignFromDeletedShopAsync(int shopId);
}
