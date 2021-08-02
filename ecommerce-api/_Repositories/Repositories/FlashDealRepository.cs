using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class FlashDealRepository : ECRepository<Flash_deal>, IFlashDealRepository
    {
        public FlashDealRepository(DataContext context) : base(context)
        {
        }
    }
}