using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CustomerPackagePaymentRepository : ECRepository<Customer_package_payment>, ICustomerPackagePaymentRepository
    {
        public CustomerPackagePaymentRepository(DataContext context) : base(context)
        {
        }
    }
}