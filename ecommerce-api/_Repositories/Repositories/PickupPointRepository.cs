using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class PickupPointRepository : ECRepository<Pickup_point>, IPickupPointRepository
    {
        public PickupPointRepository(DataContext context) : base(context)
        {
        }
    }
}