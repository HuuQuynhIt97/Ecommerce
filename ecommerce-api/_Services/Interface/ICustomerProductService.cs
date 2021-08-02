using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using ecommerce_api.Helpers.Params;

namespace ecommerce_api._Services.Interface
{
    public interface ICustomerProductService
    {
        Task<OperationResult> Create(CustomerProductDto model);
        Task<OperationResult> Update(CustomerProductDto model);
        Task<PageListUtility<CustomerProductDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CustomerProductDto> GetById(CustomerProductParam param);
        Task<OperationResult> Delete(CustomerProductParam param);
    }
}