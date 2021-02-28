using ShellLogin.Models;
using System.Collections.Generic;

namespace ShellLogin.Service
{
    public interface IUserService
    {
        User Authenticate(LoginModel model);
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
    }
}
