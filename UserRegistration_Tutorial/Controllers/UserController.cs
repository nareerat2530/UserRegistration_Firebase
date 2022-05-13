using FirebaseAdmin.Auth;
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
        //private readonly ILogger<UserController> _logger;
        
        public UserController(IUserService userService,  ILogger<UserController> logger)
        {
            _userService = userService;
            //_logger = logger;

        }
        [HttpGet]
        public IActionResult GetAllUsersAsync()
        {
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            return Ok(pagedEnumerable);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterRequest model)
        {
            _userService.RegisterAsync(model);
            return Ok(new {message = "Registration sucessful"});
        }
        
        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody]UpdateRequest model)
        {
            _userService.UpdateAsync(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpPost("Login")]
        //public IActionResult Login([FromBody] LoginRequest loginRequest)
        //{
        //    var response = _userService.Authenticate(loginRequest);
        //    return Ok(response);

        //}


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _userService.Delete(id);
            return StatusCode(200, "successfully delete");
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> GetUserById(string uid)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            return Ok(userRecord);
           
        }


    



    }
}
