using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ICustomerPackagePaymentService
    {
        Task<OperationResult> Create(CustomerPackagePaymentDto model);
        Task<OperationResult> Update(CustomerPackagePaymentDto model);
        Task<PageListUtility<CustomerPackagePaymentDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<CustomerPackagePaymentDto> GetById(int id, int userId, int customerPackageId);
        Task<OperationResult> Delete(int id, int userId, int customerPackageId);
    }
}