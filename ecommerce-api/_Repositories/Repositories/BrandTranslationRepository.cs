using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class BrandTranslationRepository : ECRepository<Brand_translation>, IBrandTranslationRepository
    {
        public BrandTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}