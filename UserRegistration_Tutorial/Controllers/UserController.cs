using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRegistration_Tutorial.Entities;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Models;

namespace UserRegistration_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        
        public UserController(IUserService userService,  ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var getAllUsers = _userService.GetAll();
            if (getAllUsers == null)
            {
                return NotFound();
            }
            return Ok(getAllUsers);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new {message = "Registration sucessful"});
        }
        
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody]UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
           var response = _userService.Authenticate(loginRequest);
           return Ok(response);

        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return StatusCode(200, "successfully delete");
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetById(id);
            if (result == null)
            {
                return NotFound("User not find");

            }

            return Ok(result);
        }


    



    }
}
