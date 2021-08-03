using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ISeoSettingService
    {
        Task<OperationResult> Create(SeoSettingDto model);
        Task<OperationResult> Update(SeoSettingDto model);
        Task<PageListUtility<SeoSettingDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<SeoSettingDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}