using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICouPonUsageService
    {
        Task<OperationResult> Create(CouponUsageDto model);
        Task<OperationResult> Update(CouponUsageDto model);
        Task<PageListUtility<CouponUsageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CouponUsageDto> GetById(int id, int userId, int couponId);
        Task<OperationResult> Delete(int id, int userId, int couponId);
    }
}