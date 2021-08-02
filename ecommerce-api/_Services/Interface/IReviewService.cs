using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;

namespace ecommerce_api._Services.Interface
{
    public interface IReviewService
    {
        Task<OperationResult> Review(ReviewDto model);
        Task<int> CountView();
    }
}