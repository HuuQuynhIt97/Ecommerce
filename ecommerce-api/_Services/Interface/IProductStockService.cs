using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IProductStockService
    {
        Task<OperationResult> Create(ProductStockDto model);
        Task<OperationResult> Update(ProductStockDto model);
        Task<PageListUtility<ProductStockDto>> GetDataWithpagination(PaginationParams param, string text, bool isPaging = true);
        Task<ProductStockDto> GetById(int id, int productId);
        Task<OperationResult> Delete(int id, int productId);
    }
}