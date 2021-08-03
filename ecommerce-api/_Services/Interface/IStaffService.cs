using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IStaffService
    {
        Task<OperationResult> Create(StaffDto model);
        Task<OperationResult> Update(StaffDto model);
        Task<PageListUtility<StaffDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<StaffDto> GetById(int id, int userId, int roleId);
        Task<OperationResult> Delete(int id, int userId, int roleId);
    }
}