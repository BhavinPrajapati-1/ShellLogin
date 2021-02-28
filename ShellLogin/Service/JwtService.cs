using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using ShellLogin.Models;

namespace ShellLogin.Service
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _expDate;
        public IConfiguration config { get; }

        public JwtService(IConfiguration Configuration)
        {
            config = Configuration;
            _secret = config["JwtConfig:secret"].ToString();
            _expDate = config["JwtConfig:expirationInMinutes"];
        }

        public string GenerateSecurityToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.DateOfBirth, user.DOB),
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
