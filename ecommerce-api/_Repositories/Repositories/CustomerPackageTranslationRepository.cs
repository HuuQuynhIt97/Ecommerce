using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CustomerPackageTranslationRepository : ECRepository<Customer_package_translation>, ICustomerPackageTranslationRepository
    {
        public CustomerPackageTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}