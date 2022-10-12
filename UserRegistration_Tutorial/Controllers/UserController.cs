using Microsoft.AspNetCore.Authorization;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
   
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
       
    }


    [HttpGet]
     // [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public IActionResult GetAllUsersAsync()
    {
        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        return Ok(pagedEnumerable);
    }


    [HttpPut("Update")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateInfoDto model)
    {
        // var email = User.Claims.Where(c => c.Type == "email")
        //     .Select(c => c.Value).SingleOrDefault();
        //
        // var userFromDataBase = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
        // var updatedUser = _userMapper.Map(model, userFromDataBase);
        // userFromDataBase = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
        return StatusCode(200,"User updated successfully");
    }


    [HttpDelete("{id}")]
     [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        await _userService.DeleteUserAsync(id);
        return StatusCode(200, "User removed successfully");
    }

    [HttpGet("{uid?}")]
    public async Task<IActionResult> GetUserById(string uid)
    {
      var getUserById = await _userService.GetUserById(uid);
      if (getUserById != null)
      {
          return Ok(getUserById);
      }

      return NotFound();

    }


    // [HttpGet("{email}")]
    // public async Task<IActionResult> GetUserByEmail(string email)
    // {
    //     var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
    //
    //     return Ok(userRecord);
    // }
}