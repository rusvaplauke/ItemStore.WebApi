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

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_itemService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_itemService.Get(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostItemDto item)
        {
            var createdItem = _itemService.Create(item);
            return Created(nameof(Post), createdItem);
        }

        [HttpPut]
        public IActionResult Put([FromBody] PutItemDto item)
        {
            return Ok(_itemService.Edit(item));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _itemService.Delete(id);
            return NoContent();
        }
    }
}
