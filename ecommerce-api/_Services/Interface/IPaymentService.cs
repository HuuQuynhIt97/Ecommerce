using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IPaymentService
    {
        Task<OperationResult> Create(PaymentDto model);
        Task<OperationResult> Update(PaymentDto model);
        Task<PageListUtility<PaymentDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<PaymentDto> GetById(int id, int sellerId);
        Task<OperationResult> Delete(int id, int sellerId);
    }
}