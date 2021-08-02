using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IShopService
    {
        Task<OperationResult> Create(ShopDto model);
        Task<OperationResult> Update(ShopDto model);
        Task<PageListUtility<ShopDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<ShopDto> GetById(int id, int userId);
        Task<OperationResult> Delete(int id, int userId);
    }
}