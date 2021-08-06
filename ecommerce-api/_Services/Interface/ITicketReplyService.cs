using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ITicketReplyService
    {
        Task<OperationResult> Create(TicketReplyDto model);
        Task<OperationResult> Update(TicketReplyDto model);
        Task<PageListUtility<TicketReplyDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<TicketReplyDto> GetById(int id, int ticketId, int userId);
        Task<OperationResult> Delete(int id, int ticketId, int userId);
    }
}