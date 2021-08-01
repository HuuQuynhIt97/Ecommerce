using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class CouponUsageRepository : ECRepository<Coupon_usages>, ICouponUsageRepository
    {
        public CouponUsageRepository(DataContext context) : base(context)
        {
        }
    }
}