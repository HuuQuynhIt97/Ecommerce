using System.Threading.Tasks;
using API.Helpers.Utilities;
using ecommerce_api.DTO;

namespace ecommerce_api._Services.Interface
{
    public interface ISubscribeService
    {
        Task<OperationResult> Subscribe(SubscribeDto model);
        Task<int> CountSubscribe();
    }
}