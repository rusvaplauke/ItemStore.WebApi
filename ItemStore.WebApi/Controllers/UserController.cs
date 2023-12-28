using ItemStore.WebApi.Models.DTOs.ItemDtos;
using ItemStore.WebApi.Models.DTOs.UserDtos;
using ItemStore.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.Controllers
{
    [ApiController]
    [Route("store/users")]
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
            return Ok(await _userService.CreateAsync(user));
        }
    }
}
