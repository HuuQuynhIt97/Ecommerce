using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IFlashDealService
    {
        Task<OperationResult> Create(FlashDealDto model);
        Task<OperationResult> Update(FlashDealDto model);
        Task<PageListUtility<FlashDealDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<FlashDealDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}