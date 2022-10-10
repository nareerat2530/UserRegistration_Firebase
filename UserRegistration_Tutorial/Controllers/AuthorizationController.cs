using Firebase.Auth;
using Firebase.Database;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.Models;
using FirebaseAuth = FirebaseAdmin.Auth.FirebaseAuth;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Authentication_Controller : ControllerBase
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
        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(user);


        return Ok(new { message = "Registration sucessful" });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAr2aApjyLC7sg8pLET0ePdsJSiGL6TLZw"));
        var auth = await authProvider.SignInWithEmailAndPasswordAsync(loginRequest.email, loginRequest.password);
        var firebase = new FirebaseClient(
            "https://firebase-with-dotnet-default-rtdb.europe-west1.firebasedatabase.app/",
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
            });

        return Ok(auth);
    }
}