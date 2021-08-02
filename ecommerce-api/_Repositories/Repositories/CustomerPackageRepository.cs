using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CustomerPackageRepository : ECRepository<Customer_package>, ICustomerPackageRepository
    {
        public CustomerPackageRepository(DataContext context) : base(context)
        {
        }
    }
}