using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IAddressService
    {
        Task<OperationResult> Create(AddressesDto model);
        Task<OperationResult> Update(AddressesDto model);
        Task<OperationResult> SetDefault(int id, int userId);
        Task<PageListUtility<AddressesDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<PageListUtility<AddressesDto>> GetDataByUserID(PaginationParams param, int userId, bool isPaging = true);
        Task<AddressesDto> GetDataByID(int id, int userId);
        Task<OperationResult> Delete(int id, int userId);
    }
}