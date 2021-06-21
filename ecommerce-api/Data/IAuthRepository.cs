using System.Threading.Tasks;
using ecommerce_api.Models;

namespace ecommerce_api.Data
{
    public interface IAuthRepository
    {
        Task<User> Login(string username, string password);
    }
}