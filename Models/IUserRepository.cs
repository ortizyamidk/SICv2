using System.Net;

namespace WPF_LoginForm.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);

        UserModel GetByUsername(string username);
    }
}
