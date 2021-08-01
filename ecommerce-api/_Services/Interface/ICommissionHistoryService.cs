using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICommissionHistoryService
    {
        Task<OperationResult> Create(CommissionHistoryDto model);
        Task<OperationResult> Update(CommissionHistoryDto model);
        Task<PageListUtility<CommissionHistoryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CommissionHistoryDto> GetById(int id, int orderId, int orderDetailId, int sellerId);
        Task<OperationResult> Delete(int id, int orderId, int orderDetailId, int sellerId);
    }
}