using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.Interfaces;

namespace UserRegistration_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BananController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtUtils _jwtUtils;


        public BananController(IUserService userService, ILogger<UserController> logger, IJwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;



        }


        [HttpGet]

        public async Task<IActionResult> GetAllBanan()
        {

            return Ok("Hej jag är en brun banan från thailand");
        }
    }
}
