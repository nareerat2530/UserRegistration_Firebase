using Firebase.Auth;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserMapper _userMapper;
    private readonly IUserService _userService;


    public AuthenticationController(IAuthService authService, UserMapper userMapper, IUserService userService)
    {
        _authService = authService;
        _userMapper = userMapper;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
    {
        await _authService.RegisterUserAsync(model);
        await _userService.AddNewUser(model);
        return StatusCode(200, "successfully add user");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAr2aApjyLC7sg8pLET0ePdsJSiGL6TLZw"));
        var auth = await authProvider.SignInWithEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
        return Ok(auth);
    }
}