using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.DTO.UserDto;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Models;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserMapper _userMapper;

    public UserController(IUserService userService, ILogger<UserController> logger, UserMapper userMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public Task<IActionResult> GetAllUsersAsync()
    {
        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        return Task.FromResult<IActionResult>(Ok(pagedEnumerable));
    }


    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UserUpdateDto model)
    {
       // var banan =  await FirebaseAuth.DefaultInstance.GetUserAsync(id);
       
        // var updatedUser = new UserRecordArgs
        // {
        //     Password = model.Password,
        //     DisplayName = model.UserName
        // };
        
        var updatedUser = _userMapper.Map(model);
     var  banan = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
        return Ok(new { message = "User updated successfully" });
    }


    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        await FirebaseAuth.DefaultInstance.DeleteUserAsync(id);
        return StatusCode(200, "successfully delete");
    }

    [HttpGet("{uid?}")]
    public async Task<IActionResult> GetUserById(string uid)
    {
        var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

        return Ok(userRecord);
    }


    [HttpGet("{menuId}/email")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);

        return Ok(userRecord);
    }
}