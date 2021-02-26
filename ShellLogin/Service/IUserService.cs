using ShellLogin.Models;
using System.Collections.Generic;

namespace ShellLogin.Service
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(LoginModel model);
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
    }
}
