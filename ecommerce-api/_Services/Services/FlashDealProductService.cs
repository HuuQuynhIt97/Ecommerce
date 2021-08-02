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
    public class FlashDealProductService : IFlashDealProductService
    {
        private readonly IFlashDealProductRepository _flashDealProductRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public FlashDealProductService(
            IFlashDealProductRepository flashDealProductRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _flashDealProductRepository = flashDealProductRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(FlashDealProductDto model)
        {
            var data = _mapper.Map<Flash_deal_product>(model);
            try
            {
                _flashDealProductRepository.Add(data);
                await _flashDealProductRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create flash deal product successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int flashDealId, int productId)
        {
            var data = await _flashDealProductRepository.FindAll(x => x.ID == id && x.Flash_deal_ID == flashDealId && x.Products_ID == productId)
                                                        .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _flashDealProductRepository.Remove(data);
                    await _flashDealProductRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete flash deal product successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
                return await Task.FromResult(operationResult);
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete flash deal product failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<FlashDealProductDto> GetById(int id, int flashDealId, int productId)
        {
            return await _flashDealProductRepository.FindAll(x => x.ID == id && x.Flash_deal_ID == flashDealId && x.Products_ID == productId)
                                                    .ProjectTo<FlashDealProductDto>(_configuration)
                                                    .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<FlashDealProductDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _flashDealProductRepository.FindAll().ProjectTo<FlashDealProductDto>(_configuration)
                                                    .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Flash_deal_ID.ToString().Contains(text) ||
                                       x.Products_ID.ToString().Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<FlashDealProductDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(FlashDealProductDto model)
        {
            var data = _mapper.Map<Flash_deal_product>(model);
            try
            {
                _flashDealProductRepository.Update(data);
                await _flashDealProductRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update flash deal product successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}