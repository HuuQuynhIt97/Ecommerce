using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class HomeCategorieRepository : ECRepository<Home_category>, IHomeCategorieRepository
    {
        public HomeCategorieRepository(DataContext context) : base(context)
        {
        }
    }
}