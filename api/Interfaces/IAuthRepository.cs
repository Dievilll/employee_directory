using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<User> CheckAuth(string username);

    }
}