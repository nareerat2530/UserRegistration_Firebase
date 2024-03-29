﻿using Microsoft.AspNetCore.Authorization;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;
using FirebaseAuth2 = FirebaseAdmin.Auth.FirebaseAuth;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserMapper _userMapper;

    public UserController(IAuthService authService, UserMapper userMapper)
    {
        _authService = authService;
        _userMapper = userMapper;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var pagedEnumerable = await FirebaseAuth.DefaultInstance.ListUsersAsync(null).ToListAsync();
        var users = _userMapper.Map(pagedEnumerable);
        return Ok(users);
    }


    [HttpPut("Update")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateInfoDto model)
    {
        await _authService.UpdateUserAsync(model);
        return StatusCode(200, "User updated successfully");
    }


    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        await _authService.DeleteUserAsync(id);
        return StatusCode(200, "User removed successfully");
    }

    [HttpGet("{uid?}")]
    public async Task<IActionResult> GetUserById(string uid)
    {
        var getUserById = await _authService.GetUserById(uid);
        if (getUserById != null) return Ok(getUserById);

        return NotFound();
    }
    
}