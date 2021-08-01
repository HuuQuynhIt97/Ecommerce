using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class AttributeTranslationRepository : ECRepository<Attribute_translation>, IAttributeTranslationRepository
    {
        public AttributeTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}