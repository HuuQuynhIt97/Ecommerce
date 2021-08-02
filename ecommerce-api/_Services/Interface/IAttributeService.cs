using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IAttributeService
    {
        Task<OperationResult> Create(AttributeDto model);
        Task<OperationResult> Update(AttributeDto model);
        Task<PageListUtility<AttributeDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<AttributeDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}