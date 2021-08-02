using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CategoryTranslationRepository : ECRepository<Category_translation>, ICategoryTranslationRepository
    {
        public CategoryTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}