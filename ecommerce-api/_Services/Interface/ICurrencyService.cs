using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICurrencyService
    {
        Task<OperationResult> Create(CurrencyDto model);
        Task<OperationResult> Update(CurrencyDto model);
        Task<PageListUtility<CurrencyDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CurrencyDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}