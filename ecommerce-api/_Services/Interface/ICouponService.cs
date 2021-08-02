using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICouponService
    {
        Task<OperationResult> Create(CouponDto model);
        Task<OperationResult> Update(CouponDto model);
        Task<PageListUtility<CouponDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CouponDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}