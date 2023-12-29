using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces;

public interface IPurchaseRepository
{
    Task<PurchaseDto> BuyAsync (PurchaseDto purchase);

    Task<List<int>> GetPurchasesByUserAsync(PurchaseDto purchase);
}
