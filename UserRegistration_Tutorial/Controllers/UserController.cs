using Microsoft.AspNetCore.Authorization;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserMapper _userMapper;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ILogger<UserController> logger, UserMapper userMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public IActionResult GetAllUsersAsync()
    {
        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        return Ok(pagedEnumerable);
    }


    [HttpPut("Update")]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateInfoDto model)
    {
        var email = User.Claims.Where(c => c.Type == "email")
            .Select(c => c.Value).SingleOrDefault();

        var userFromDataBase = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);


        var updatedUser = _userMapper.Map(model, userFromDataBase);
        userFromDataBase = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
        return StatusCode(200,"User updated successfully");
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


    [HttpGet("{email?}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);

        return Ok(userRecord);
    }
}