using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IBlogCategoryService
    {
        Task<OperationResult> Create(Blog_categoryDto model);
        Task<OperationResult> Update(Blog_categoryDto model);
        Task<OperationResult> Delete(int id);
        Task<PageListUtility<Blog_categoryDto>> GetDataWithPaination(PaginationParams param, string text, bool isPaging = true);
    }
}