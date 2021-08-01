using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CustomerProductRepository : ECRepository<Customer_product>, ICustomerProductRepository
    {
        public CustomerProductRepository(DataContext context) : base(context)
        {
        }
    }
}