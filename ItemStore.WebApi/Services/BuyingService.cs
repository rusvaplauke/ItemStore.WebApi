using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using System.Numerics;

namespace ItemStore.WebApi.Services
{
    public class BuyingService : IBuyingService
    {
        private readonly IItemRepository _itemRepository;
        public BuyingService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public DiscountResponseDto BuyWithDiscount(DiscountRequestDto request)
        {
            // calculate discount
            decimal discount = 0;
            if (request.Quantity >= 20)
                discount = 0.2M;
            else if (request.Quantity >= 10)
                discount = 0.1M;

            // create object to return total price
            GetItemDto item = _itemRepository.GetItem(request.ItemId);
            decimal totalPrice = item.Price * request.Quantity * (1 - discount);

            return new DiscountResponseDto(request, totalPrice);
        }
    }
}
