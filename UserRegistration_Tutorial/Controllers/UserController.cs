using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.Interfaces;

namespace UserRegistration_Tutorial.Controllers
{
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
        public IActionResult GetAll()
        {
            var getAllUsers = _userService.GetAll();
            if(getAllUsers == null)
            {
                return NotFound();
            }
            return Ok(getAllUsers);
        }
    }
}
