using Firebase.Auth;
using Microsoft.AspNetCore.Identity;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Models.Users;
using UserRegistration_Tutorial.Services;
using FirebaseAuth = FirebaseAdmin.Auth.FirebaseAuth;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserMapper _userMapper;
    


    public AuthenticationController(IUserService userService, UserMapper userMapper)
    {
        _userService = userService;
        _userMapper = userMapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
    {
     
        await _userService.RegisterUserAsync(model);
        return StatusCode(200,"successfully add user");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAr2aApjyLC7sg8pLET0ePdsJSiGL6TLZw"));
        var auth = await authProvider.SignInWithEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
        return Ok(auth);
    }
}