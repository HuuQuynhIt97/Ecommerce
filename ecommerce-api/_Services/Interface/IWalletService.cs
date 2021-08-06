using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IWalletService
    {
        Task<OperationResult> Create(WalletDto model);
        Task<OperationResult> Update(WalletDto model);
        Task<PageListUtility<WalletDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<WalletDto> GetById(int id, int userId);
        Task<OperationResult> Delete(int id, int userId);
    }
}