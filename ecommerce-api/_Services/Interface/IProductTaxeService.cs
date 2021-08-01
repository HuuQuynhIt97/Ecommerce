using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IProductTaxeService
    {
        Task<OperationResult> Create(ProductTaxeDto model);
        Task<OperationResult> Update(ProductTaxeDto model);
        Task<PageListUtility<ProductTaxeDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<ProductTaxeDto> GetById(int id, int productId, int taxeId);
        Task<OperationResult> Delete(int id, int productId, int taxeId);
    }
}