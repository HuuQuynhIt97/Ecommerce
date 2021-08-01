using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IBlogService
    {
        Task<OperationResult> Create(BlogDto model);
        Task<OperationResult> Update(BlogDto model);
        Task<OperationResult> Delete(int id, int cateId);
        Task<PageListUtility<BlogDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
    }
}