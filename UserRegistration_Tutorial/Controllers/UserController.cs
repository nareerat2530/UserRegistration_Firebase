using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
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
       
        
        
        public UserController(IUserService userService,  ILogger<UserController> logger)
        {
            _userService = userService;
           
           

        }
        [Authorize]
        [HttpGet]
       
        public async Task<IActionResult> GetAllUsersAsync()
        {

            var token = Request.Cookies["auth._token.local"]?.Split(" ").Last();
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);

            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            string uid = decodedToken.Uid;
            return Ok(pagedEnumerable);
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

       

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {

            await FirebaseAuth.DefaultInstance.DeleteUserAsync(id);
            return StatusCode(200, "successfully delete");
        }

        [HttpGet("{uid?}")]
        public async Task<IActionResult> GetUserById(string uid)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

            return Ok(userRecord);

        }

      

        [HttpGet("{menuId}/email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);

            return Ok(userRecord);

        }
       






    }
}
