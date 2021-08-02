using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class RoleTranslationRepository : ECRepository<Role_translation>, IRoleTranslationRepository
    {
        public RoleTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}