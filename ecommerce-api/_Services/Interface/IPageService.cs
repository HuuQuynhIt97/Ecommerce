using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IPageService
    {
        Task<OperationResult> Create(PageDto model);
        Task<OperationResult> Update(PageDto model);
        Task<PageListUtility<PageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<PageDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}