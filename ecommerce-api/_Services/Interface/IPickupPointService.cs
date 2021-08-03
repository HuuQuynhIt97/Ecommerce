using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IPickupPointService
    {
        Task<OperationResult> Create(PickupPointDto model);
        Task<OperationResult> Update(PickupPointDto model);
        Task<PageListUtility<PickupPointDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<PickupPointDto> GetById(int id, int staffId);
        Task<OperationResult> Delete(int id, int staffId);
    }
}