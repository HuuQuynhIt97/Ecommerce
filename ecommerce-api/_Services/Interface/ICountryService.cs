using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICountryService
    {
        Task<OperationResult> Create(CountryDto model);
        Task<OperationResult> Update(CountryDto model);
        Task<PageListUtility<CountryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CountryDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}