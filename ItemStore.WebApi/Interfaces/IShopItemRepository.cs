using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces;

public interface IShopItemRepository
{
    Task AssignToStoreAsync(ShopItemDto shopItem);
}
