using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ISellerService
    {
        Task<OperationResult> Create(SellerDto model);
        Task<OperationResult> Update(SellerDto model);
        Task<PageListUtility<SellerDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<SellerDto> GetById(int id, int userId);
        Task<OperationResult> Delete(int id, int userId);
    }
}