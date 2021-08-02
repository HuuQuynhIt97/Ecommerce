using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICityService
    {
        Task<OperationResult> Create(CityDto model);
        Task<OperationResult> Update(CityDto model);
        Task<PageListUtility<CityDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CityDto> GetByID(int id, int countryId);
        Task<OperationResult> Delete(int id, int countryId);
    }
}