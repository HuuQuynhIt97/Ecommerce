using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICustomerPackageService
    {
        Task<OperationResult> Create(CustomerPackageDto model);
        Task<OperationResult> Update(CustomerPackageDto model);
        Task<PageListUtility<CustomerPackageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CustomerPackageDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}