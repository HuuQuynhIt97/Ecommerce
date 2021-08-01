using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IBannerService
    {
        Task<OperationResult> Create(BannerDto model);
        Task<OperationResult> Update(BannerDto model);
        Task<PageListUtility<BannerDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<BannerDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}