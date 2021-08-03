using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ITicketService
    {
        Task<OperationResult> Create(TicketDto model);
        Task<OperationResult> Update(TicketDto model);
        Task<PageListUtility<TicketDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<TicketDto> GetById(int id, int userId);
        Task<OperationResult> Delete(int id, int userId);
    }
}