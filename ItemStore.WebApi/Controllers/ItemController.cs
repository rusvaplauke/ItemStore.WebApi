using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.ItemDtos;
using ItemStore.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.Controllers
{
    [ApiController]
    [Route("store/items")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService) => _itemService = itemService;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _itemService.Get());


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _itemService.Get(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostItemDto item)
        {
            var createdItem = await _itemService.Create(item);
            return Created(nameof(Post), createdItem);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutItemDto item) => Ok(await _itemService.Edit(item));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.Delete(id);
            return NoContent();
        }
    }
}
