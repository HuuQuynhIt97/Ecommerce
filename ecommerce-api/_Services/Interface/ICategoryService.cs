using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICategoryService
    {
        Task<OperationResult> Create(CategoryDto model);
        Task<OperationResult> Update(CategoryDto model);
        Task<PageListUtility<CategoryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
    }
}