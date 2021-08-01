using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IPolicyService
    {
        Task<OperationResult> Create(PolicyDto model);
        Task<OperationResult> Update(PolicyDto model);
        Task<OperationResult> Delete(int id);
        Task<PageListUtility<PolicyDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
    }
}