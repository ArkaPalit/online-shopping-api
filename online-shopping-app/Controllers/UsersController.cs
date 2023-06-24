using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_shopping_app.Models;
using online_shopping_app.Services;

namespace online_shopping_app.Controllers
{
    [Route("/api/v1.0/shopping")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("register")]
        public ActionResult<Product> RegisterNewUser([FromBody] User user)
        {
            var result = userService.UserRegistration(user);
            if(result != null)
            {
                return Ok(new { success = true, messaege = "Request is successful" });
            }
            else
            {
                return NotFound(new { success = false, message = "Invalid request" });
            }
           
        }
        [Authorize]
        [HttpGet("user/{id}")]
        public ActionResult<User> GetUserById(string id)
        {
            var user = userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { success = false, message = "Invalid request" });
            }
            return Ok(new { data = user, success = true, messaege = "Request is successful" });
        }

        [HttpPost("login")]
        public IActionResult Login(User users)
        {
            var user = userService.AuthenticateUser(users);
            if (user != null)
            {
                var token = userService.GenerateToken(user);
                return Ok(new { token = token, userName = user.LoginId, success = true, messaege = "Request is successful" });
            }
            return Unauthorized(new { success = false, message = "Invalid request" });
        }
        [Authorize]
        [HttpPut("{userName}/forgot")]
        public IActionResult ForgotPassword(string userName, [FromBody] User user)
        {
            var loggedinUser = userService.GetUserByUserName(userName);
            if (loggedinUser == null)
            {
                return NotFound(new { success = false, message = "Invalid request" });
            }
            userService.ForgotPassword(userName, user);
            return Ok(new { success = true, messaege = "Request is successful"});
        }

    }
}
