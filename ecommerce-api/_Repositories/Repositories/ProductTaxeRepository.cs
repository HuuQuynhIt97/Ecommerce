using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class ProductTaxeRepository : ECRepository<Product_taxe>, IProductTaxeRepository
    {
        public ProductTaxeRepository(DataContext context) : base(context)
        {
        }
    }
}