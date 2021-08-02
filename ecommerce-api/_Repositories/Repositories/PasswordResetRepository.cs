using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class PasswordResetRepository : ECRepository<Password_reset>, IPasswordResetRepository
    {
        public PasswordResetRepository(DataContext context) : base(context)
        {
        }
    }
}