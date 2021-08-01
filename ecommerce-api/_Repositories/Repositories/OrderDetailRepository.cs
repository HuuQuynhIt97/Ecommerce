using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class OrderDetailRepository : ECRepository<Order_detail>, IOrderDetailRepository
    {
        public OrderDetailRepository(DataContext context) : base(context)
        {
        }
    }
}