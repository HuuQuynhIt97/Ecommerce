using ecommerce_api._Repositories.Interface;
using ecommerce_api.Data;
using ecommerce_api.Models;

namespace ecommerce_api._Repositories.Repositories
{
    public class BlogCategoryRepository : ECRepository<Blog_category>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(DataContext context) : base(context)
        {
        }
    }
}