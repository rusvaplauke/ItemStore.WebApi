using ItemStore.WebApi.Models.DTOs.ItemDtos;
using ItemStore.WebApi.Models.DTOs.ShopDtos;
using ItemStore.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.Controllers;

[ApiController]
[Route("shops")]
public class ShopController : ControllerBase
{
    private readonly ShopService _shopService;
    public ShopController(ShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _shopService.GetAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _shopService.GetAsync(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostShopDto shop)
    {
        var createdShop = await _shopService.CreateAsync(shop);
        return Created(nameof(Post), createdShop);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PutShopDto shop)
    {
        return Ok(await _shopService.EditAsync(shop));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _shopService.DeleteAsync(id);
        return NoContent();
    }
}
