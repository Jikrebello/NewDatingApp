using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.Services.Interfaces;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet(template: "{id}")]
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
    public async Task<IActionResult> CreateUser(RegisterUserDTO dto)
    {
        var result = await _usersService.Create(dto);
        if (result.Success)
        {
            return Created("", new { result.Message, result.Success });
        }
        return BadRequest(new { result.Message });
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO dto)
    {
        var result = await _usersService.Login(dto);
        if (result.Success)
        {
            return Ok(result);
        }
        return Unauthorized(new { result.Message });
    }
}
