using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web.Resource;
using ShellLogin.Models;
using ShellLogin.Service;
using System;
using System.Linq;

namespace ShellLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

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

        [Authorize]
        [HttpGet]
        public int Get()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            
            var rng = new Random();
            return rng.Next();
        }
    }
}
