using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.Controllers
{
    [ApiController]
    [Route("store/purchases")]
    public class BuyingController : ControllerBase
    {
        private readonly IBuyingService _buyingService;
        private readonly ILogger<ItemController> _logger;

        public BuyingController(IBuyingService itemService, ILogger<ItemController> logger)
        {
            _buyingService = itemService;
            _logger = logger;
        }

        [HttpPost("buy/{id}")]
        public IActionResult Post(int id, int quantity)
        {
            return Ok(_buyingService.BuyWithDiscount(new DiscountRequestDto { Quantity = quantity, ItemId = id })); // what to return in Create??
        }
    }
}
