using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CityTranslationRepository : ECRepository<City_translation>, ICityTranslationRepository
    {
        public CityTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}