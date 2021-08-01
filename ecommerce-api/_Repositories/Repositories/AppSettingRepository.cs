using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class AppSettingRepository : ECRepository<App_setting>, IAppSettingRepository
    {
        public AppSettingRepository(DataContext context) : base(context)
        {
        }
    }
}