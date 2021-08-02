using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IGeneralSettingService
    {
        Task<OperationResult> Create(GeneralSettingDto model);
        Task<OperationResult> Update(GeneralSettingDto model);
        Task<PageListUtility<GeneralSettingDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<GeneralSettingDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}