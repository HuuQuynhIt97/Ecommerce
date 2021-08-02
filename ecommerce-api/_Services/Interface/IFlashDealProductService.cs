using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IFlashDealProductService
    {
        Task<OperationResult> Create(FlashDealProductDto model);
        Task<OperationResult> Update(FlashDealProductDto model);
        Task<PageListUtility<FlashDealProductDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<FlashDealProductDto> GetById(int id, int flashDealId, int productId);
        Task<OperationResult> Delete(int id, int flashDealId, int productId);
    }
}