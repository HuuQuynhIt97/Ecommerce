using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class SellerWithdrawRequestRepository : ECRepository<Seller_withdraw_request>, ISellerWithdrawRequestRepository
    {
        public SellerWithdrawRequestRepository(DataContext context) : base(context)
        {
        }
    }
}