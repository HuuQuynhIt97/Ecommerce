using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ISellerWithdrawRequestService
    {
        Task<OperationResult> Create(SellerWithdrawRequestDto model);
        Task<OperationResult> Update(SellerWithdrawRequestDto model);
        Task<PageListUtility<SellerWithdrawRequestDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<SellerWithdrawRequestDto> GetById(int id, int userId);
        Task<OperationResult> Delete(int id, int userId);
    }
}