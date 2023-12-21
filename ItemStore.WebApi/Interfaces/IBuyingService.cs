using ItemStore.WebApi.Models.DTOs;

namespace ItemStore.WebApi.Interfaces
{
    public interface IBuyingService
    {
       public Task<DiscountResponseDto> BuyWithDiscount(DiscountRequestDto request);
    }
}
