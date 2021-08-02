using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class ProductTranslationRepository : ECRepository<Product_translation>, IProductTranslationRepository
    {
        public ProductTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}