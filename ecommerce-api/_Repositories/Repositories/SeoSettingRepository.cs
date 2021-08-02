using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class SeoSettingRepository : ECRepository<Seo_setting>, ISeoSettingRepository
    {
        public SeoSettingRepository(DataContext context) : base(context)
        {
        }
    }
}