using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShellLogin.Models;
using ShellLogin.Service;

namespace ShellLogin.Controllers
{
    [Route("api/home")]
    [Authorize]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public HomeController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginModel request)
        {
            var response = _userService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            return  Ok(_jwtService.GenerateSecurityToken(user: response));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
