using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface IColorService
    {
        Task<OperationResult> Create(ColorDto model);
        Task<OperationResult> Update(ColorDto model);
        Task<PageListUtility<ColorDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<ColorDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}