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
            //Users.FirstOrDefault(u => u.UserName == model.UserName || u.PasswordHash == model.Password);
            //var registerUser =  _userService.RegisterAsync(model);
            //if(registerUser == null && User.FirstOrDefault(u => u.UserName == model.UserName))
            //{
            //    return StatusCode(500, "Email or Password is invalid");
            //}
            
            //return Ok(new {message = "Registration sucessful"});
            return Ok(model);
        }
        
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody]UpdateRequest model)
        {
          
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(id);
                UserRecordArgs updatedUser = new UserRecordArgs()
                {

                    Email = model.Email,
                    Password = model.Password,
                    DisplayName = model.UserName,

                };
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
                return Ok(new { message = "User updated successfully" });
            
         
            
             
        }

        [HttpPost("Login")]
        //public IActionResult Login([FromBody] LoginRequest loginRequest)
        //{
        //    var response = _userService.Authenticate(loginRequest);
        //    return Ok(response);

        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {

            await FirebaseAuth.DefaultInstance.DeleteUserAsync(id);
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
