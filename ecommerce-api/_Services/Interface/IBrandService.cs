using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IBrandService
    {
        Task<OperationResult> Create(BrandDto model);
        Task<OperationResult> Update(BrandDto model);
        Task<OperationResult> Delete(int id);
        Task<PageListUtility<BrandDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
    }
}