using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class FlashDealProductRepository : ECRepository<Flash_deal_product>, IFlashDealProductRepository
    {
        public FlashDealProductRepository(DataContext context) : base(context)
        {
        }
    }
}