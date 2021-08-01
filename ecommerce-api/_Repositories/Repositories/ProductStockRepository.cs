using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class ProductStockRepository : ECRepository<Product_stock>, IProductStockRepository
    {
        public ProductStockRepository(DataContext context) : base(context)
        {
        }
    }
}