using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class LanguageRepository : ECRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(DataContext context) : base(context)
        {
        }
    }
}