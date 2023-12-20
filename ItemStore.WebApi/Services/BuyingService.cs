using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using System.Numerics;

namespace ItemStore.WebApi.Services
{
    public class BuyingService : IBuyingService
    {
        private readonly IItemService _itemService;
        public BuyingService(IItemService itemService)
        {
            _itemService = itemService;
        }
        public DiscountResponseDto BuyWithDiscount(DiscountRequestDto request)
        {
            // calculate discount
            decimal afterDisc = 1;
            if (request.Quantity >= 20)
                afterDisc = 0.8M;
            else if (request.Quantity >= 10)
                afterDisc = 0.9M;

            // create object to return total price
            GetItemDto item = _itemService.Get(request.ItemId);
            if (item == null)
                throw new ArgumentNullException($"Product with id {request.ItemId} not found.");

            return new DiscountResponseDto(request, item.Price * request.Quantity * afterDisc);
        }
    }
}
