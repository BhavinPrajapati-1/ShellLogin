using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellLogin.Middleware
{
    public static class AuthenticationExtension
    {
        static AuthenticationExtension()
        {
        }

        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var secret = config.GetSection("JwtConfig").GetSection("secret").Value;

            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost"
                };
            });

            return services;
        }

        public static IServiceCollection JWTMechanism(this IServiceCollection services, IConfiguration config)
        {
            string JwtSecretKey = config.GetSection("JwtConfig").GetSection("secret").Value;
            var encryptionKey = Encoding.ASCII.GetBytes(JwtSecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer2", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(encryptionKey),
                    ValidateAudience = false,

                    //ValidateIssuerSigningKey = true,
                    //ValidateIssuer = true,
                    //ValidIssuer = config["Jwt:Issuer"],
                    //ValidateAudience = true,
                    //ValidAudience = config["Jwt:Issuer"],
                    //ValidateLifetime = true,
                };
            });
            
            return services;
        }
    }
}
