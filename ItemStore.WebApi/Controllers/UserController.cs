using ItemStore.WebApi.Models.DTOs;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ItemStore.WebApi.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userService.GetAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _userService.GetAsync(id));
    }

    [HttpPost] 
    public async Task<IActionResult> Post([FromBody] PostUserDto user)
    {
        GetUserDto createdUser = await _userService.CreateAsync(user);
        return Created(nameof(Post), createdUser);
    }

    [HttpPost("{id}/buy")] 
    public async Task<IActionResult> Buy([FromRoute]int id, int itemId)
    {
        PurchaseDto createdPurchase = await _userService.BuyAsync(new PurchaseDto {UserId = id, ItemId = itemId});
        return Created(nameof(Post), createdPurchase);
    }
}

