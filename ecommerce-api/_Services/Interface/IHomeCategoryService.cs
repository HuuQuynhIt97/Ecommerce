using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IHomeCategoryService
    {
        Task<OperationResult> Create(HomeCategoryDto model);
        Task<OperationResult> Update(HomeCategoryDto model);
        Task<PageListUtility<HomeCategoryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<HomeCategoryDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}