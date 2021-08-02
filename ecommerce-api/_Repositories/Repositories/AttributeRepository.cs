

using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class AttributeRepository : ECRepository<Attributes>, IAttributeRepository
    {
        public AttributeRepository(DataContext context) : base(context)
        {
        }
    }
}