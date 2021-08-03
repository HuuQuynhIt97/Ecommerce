using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IOrderService
    {
        Task<OperationResult> Create(OrderDto model);
        Task<OperationResult> Update(OrderDto model);
        Task<PageListUtility<OrderDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<OrderDto> GetById(int id, int userId, int guestId, int seller);
        Task<OperationResult> Delete(int id, int userId, int guestId, int seller);
    }
}