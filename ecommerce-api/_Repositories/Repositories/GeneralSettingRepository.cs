using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class GeneralSettingRepository : ECRepository<General_setting>, IGeneralSettingRepository
    {
        public GeneralSettingRepository(DataContext context) : base(context)
        {
        }
    }
}