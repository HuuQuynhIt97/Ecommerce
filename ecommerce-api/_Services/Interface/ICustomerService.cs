using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICustomerService
    {
        Task<OperationResult> Create(CustomerDto model);
        Task<OperationResult> Update(CustomerDto model);
        Task<PageListUtility<CustomerDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CustomerDto> GetById(int id, int userId);
        Task<OperationResult> Delete(int id, int userId);
    }
}