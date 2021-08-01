using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class WishlistRepository : ECRepository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(DataContext context) : base(context)
        {
        }
    }
}