using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;

namespace ecommerce_api._Services.Interface
{
    public interface ISliderService
    {
        Task<OperationResult> Create(SliderDto model);
        Task<OperationResult> Update(SliderDto model);
        Task<PageListUtility<SliderDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true);
        Task<SliderDto> GetById(int id);
        Task<OperationResult> Delete(int id);
    }
}