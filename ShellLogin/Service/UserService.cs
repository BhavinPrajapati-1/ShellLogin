using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShellLogin.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ShellLogin.Service
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test1", LastName = "Test1", Username = "test1", DOB="1995/07/15", Email="avbc@bc.com",Role="Nurse", Password = "test1" },
            new User { Id = 2, FirstName = "Test2", LastName = "Test2", Username = "test2", DOB="1991/01/15", Email="avbc@bc.com",Role="Dentist", Password = "test2" },
            new User { Id = 3, FirstName = "Test3", LastName = "Test3", Username = "test3", DOB="1999/02/29", Email="avbc@bc.com",Role="Doctor", Password = "test3" }
        };

        public UserService()
        {
        }

        public User Authenticate(LoginModel model)
        {
            return _users.SingleOrDefault(x => x.Username == model.UserName && x.Password == model.Password);
        }

        IEnumerable<User> IUserService.GetAllUsers()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

    }
}
