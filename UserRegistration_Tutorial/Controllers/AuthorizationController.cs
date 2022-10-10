using Firebase.Auth;
using UserRegistration_Tutorial.Models.Users;
using FirebaseAuth = FirebaseAdmin.Auth.FirebaseAuth;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest model)
    {
        var user = new UserRecordArgs
        {
            Email = model.Email,
            Password = model.Password,
            DisplayName = model.UserName
        };
        await FirebaseAuth.DefaultInstance.CreateUserAsync(user);

        return Ok(new { message = "Registration sucessful" });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAr2aApjyLC7sg8pLET0ePdsJSiGL6TLZw"));
        var auth = await authProvider.SignInWithEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
        return Ok(auth);
    }
}