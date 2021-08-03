using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IOrderDetailService
    {
        Task<OperationResult> Create(OrderDetailDto model);
        Task<OperationResult> Update(OrderDetailDto model);
        Task<PageListUtility<OrderDetailDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<OrderDetailDto> GetById(int id, int orderId, int sellerId, int productId);
        Task<OperationResult> Delete(int id, int orderId, int sellerId, int productId);
    }
}