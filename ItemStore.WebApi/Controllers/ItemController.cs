using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.Controllers
{
    [ApiController]
    [Route("store/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_itemService.GetItems());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = _itemService.GetItem(id);
                return Ok(item);
            }
            catch (ArgumentException ex) 
            {
                string message = $"Item with id {id} not found.";
                _logger.LogWarning(message);
                return NotFound(message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostItemDto item)
        {
            _itemService.CreateItem(item); // TODO: return URI, object
            return Created();
        }

        [HttpPut]
        public IActionResult Put([FromBody] PutItemDto item)
        {
            return Ok(_itemService.EditItem(item));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _itemService.DeleteItem(id);
                return NoContent(); //TODO: retrun id in message
            }
            catch (ArgumentException ex)
            {
                string message = $"Item with id {id} not found.";
                _logger.LogWarning(message);
                return NotFound(message);
            }
        }
    }
}
