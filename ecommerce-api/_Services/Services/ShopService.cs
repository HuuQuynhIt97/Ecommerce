using System.Linq;
using System.Threading.Tasks;
using API.Helpers.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api._Services.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public ShopService(
            IShopRepository shopRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(ShopDto model)
        {
            var data = _mapper.Map<Shop>(model);
            try
            {
                _shopRepository.Add(data);
                await _shopRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create shop successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            var data = await _shopRepository.FindAll(x => x.ID == id && x.User_ID == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _shopRepository.Remove(data);
                    await _shopRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete shop successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete shop failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<ShopDto> GetById(int id, int userId)
        {
            return await _shopRepository.FindAll(x => x.ID == id && x.User_ID == userId)
                                        .ProjectTo<ShopDto>(_configuration)
                                        .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<ShopDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _shopRepository.FindAll().ProjectTo<ShopDto>(_configuration).OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<ShopDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(ShopDto model)
        {
            var data = _mapper.Map<Shop>(model);
            try
            {
                _shopRepository.Update(data);
                await _shopRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update shop successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}