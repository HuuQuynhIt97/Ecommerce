using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class ReviewRepository : ECRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DataContext context) : base(context)
        {
        }
    }
}