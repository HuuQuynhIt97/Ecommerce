using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IProductService
    {
        Task<OperationResult> Create(ProductDto model);
        Task<OperationResult> Update(ProductDto model);
        Task<PageListUtility<ProductDto>> GetDataWithPagination(PaginationParams paginationParams, string text, bool isPaging = true);
        Task<ProductDto> GetById(int id, int cateId, int brandId);
        Task<OperationResult> Delete(int id, int cateId, int brandId);
    }
}