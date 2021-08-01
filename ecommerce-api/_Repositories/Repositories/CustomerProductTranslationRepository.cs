using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CustomerProductTranslationRepository : ECRepository<Customer_product_translation>, ICustomerProductTranslationRepository
    {
        public CustomerProductTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}