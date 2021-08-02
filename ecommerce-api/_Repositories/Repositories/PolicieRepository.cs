using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class PolicieRepository : ECRepository<Policy>, IPolicieRepository
    {
        public PolicieRepository(DataContext context) : base(context)
        {
        }
    }
}