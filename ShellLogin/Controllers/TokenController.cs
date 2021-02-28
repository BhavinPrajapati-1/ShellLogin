using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShellLogin.Models;
using ShellLogin.Service;

namespace ShellLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public TokenController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        /// <summary>
        /// to Generates token Without Authentication.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Generates and Return token Without Authentication.</returns>
        [HttpPost]
        public string GetRandomToken(User user)
        {
            var token = _jwtService.GenerateSecurityToken(user);
            return token;
        }
    }
}
