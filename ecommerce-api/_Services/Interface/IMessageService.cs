using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IMessageService
    {
        Task<OperationResult> Create(MessageDto model);
        Task<OperationResult> Update(MessageDto model);
        Task<PageListUtility<MessageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<MessageDto> GetById(int id, int conversationId, int userId);
        Task<OperationResult> Delete(int id, int conversationId, int userId);
    }
}