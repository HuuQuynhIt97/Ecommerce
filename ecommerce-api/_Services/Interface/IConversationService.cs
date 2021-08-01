using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IConversationService
    {
        Task<OperationResult> Create(ConversationDto model);
        Task<OperationResult> Update(ConversationDto model);
        Task<PageListUtility<ConversationDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<ConversationDto> GetById(int id, int senderId, int receiverId);
        Task<OperationResult> Delete(int id, int senderId, int receiverId);
    }
}