using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_api._Services.Interface
{
   public interface IServiceBase<T, TDto> 
    {
        Task<bool> Add(TDto model);

        Task<bool> Update(TDto model);

        Task<bool> Delete(object id);

        Task<List<TDto>> GetAllAsync();

        TDto GetById(object id);
    }
}
