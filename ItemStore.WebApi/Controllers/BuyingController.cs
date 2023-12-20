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

        //[HttpPost("buy/{id}")]
        //public IActionResult Get([FromBody] DiscountRequestDto request)
        //{
        //    try
        //    {
        //        return Ok(_buyingService.BuyWithDiscount(request));
        //    }
        //    catch (ArgumentException ex) 
        //    {
        //        string message = $"Product with id {request.ItemId} not found.";
        //        _logger.LogWarning(message);
        //        return NotFound(message);
        //    }
        //}
    }
}
