using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CouponRepository : ECRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(DataContext context) : base(context)
        {
        }
    }
}