using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ILinkService
    {
        Task<OperationResult> Create(LinkDto model);
        Task<OperationResult> Update(LinkDto model);
        Task<PageListUtility<LinkDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<LinkDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}