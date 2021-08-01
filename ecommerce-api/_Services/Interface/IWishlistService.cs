using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IWishlistService
    {
        Task<OperationResult> Create(WishlistDto model);
        Task<OperationResult> Delete(int id, int productId, int userId);
        Task<PageListUtility<WishlistDto>> GetDataWithPagination(PaginationParams param, bool isPaging = true);
    }
}