using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Models;

namespace UserRegistration_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication_Controller : ControllerBase
    {
        private readonly IJwtUtils _jwtUtils;

        public Authentication_Controller(IJwtUtils jwtUtils )
        {
            _jwtUtils = jwtUtils;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            //return Ok(_jwtUtils.GenerateToken( loginRequest));

            var client = new HttpClient();

            var logger = new LoginRequest
            {
                email = loginRequest.email,
                password = loginRequest.password,
                returnSecureToken = loginRequest.returnSecureToken,
            };

            //var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?keyAIzaSyAr2aApjyLC7sg8pLET0ePdsJSiGL6TLZw {logger}";



            var bulle = await client.PostAsJsonAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyAr2aApjyLC7sg8pLET0ePdsJSiGL6TLZw", logger);
            var responseString = await bulle.Content.ReadAsStringAsync();
            return Ok(responseString);


        }
    }
}
