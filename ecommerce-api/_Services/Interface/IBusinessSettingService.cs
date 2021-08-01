using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IBusinessSettingService
    {
        Task<OperationResult> Create(Business_settingDto model);
        Task<OperationResult> Update(Business_settingDto model);
        Task<PageListUtility<Business_settingDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<Business_settingDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}