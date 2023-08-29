using Microsoft.AspNetCore.Mvc;
using API.ViewModels;
using API.Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/users/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _usersService.GetByIdAsync(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return NotFound(new { result.Message });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _usersService.GetAllAsync();
        if (result.Success)
        {
            return Ok(result);
        }
        return NotFound(new { result.Message });
    }

    [HttpPost]
    public async Task<IActionResult> InsertUser([FromBody] UserViewModel model)
    {
        var result = await _usersService.Insert(model);
        if (result.Success)
        {
            return Created("", new { result.Message, result.Success });
        }
        return BadRequest(new { result.Message });
    }
}
