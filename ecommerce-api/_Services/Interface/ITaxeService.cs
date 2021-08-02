using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ITaxeService
    {
        Task<OperationResult> Create(TaxeDto model);
        Task<OperationResult> Update(TaxeDto model);
        Task<PageListUtility<TaxeDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<TaxeDto> GetById(int id);
        Task<OperationResult> Delete(int id);
        Task<OperationResult> ChangeStatus(int id);
    }
}