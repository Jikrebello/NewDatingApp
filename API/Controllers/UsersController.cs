using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet(template: "{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
    {
        var result = await _usersService.GetByIdAsync(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return NotFound(new { result.Message });
    }

    [HttpGet(template: "{emailAddress}")]
    public async Task<ActionResult<UserDTO>> GetUserByEmailAddress(string emailAddress)
    {
        var result = await _usersService.GetByEmailAddressAsync(emailAddress);
        if (result.Success)
        {
            return Ok(result);
        }
        return NotFound(new { result.Message });
    }

    [HttpGet]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<ActionResult<UserDTO>> RegisterUser(RegisterUserDTO dto)
    {
        var result = await _usersService.Register(dto);
        if (result.Success)
        {
            return Created("", result);
        }
        return BadRequest(new { result.Message });
    }

    [HttpPost]
    [AllowAnonymous]
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
