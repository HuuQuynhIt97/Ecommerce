using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class PickupPointTranslationRepository : ECRepository<Pickup_point_translation>, IPickupPointTranslationRepository
    {
        public PickupPointTranslationRepository(DataContext context) : base(context)
        {
        }
    }
}