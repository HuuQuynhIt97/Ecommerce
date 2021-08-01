using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class FlashDealTranslationRepository : ECRepository<Flash_deal_translation>, IFlashDealTranslationRepository
    {
        public FlashDealTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}