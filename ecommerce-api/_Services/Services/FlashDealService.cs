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
    public class FlashDealService : IFlashDealService
    {
        private readonly IFlashDealRepository _flashDealRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public FlashDealService(
            IFlashDealRepository flashDealRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _flashDealRepository = flashDealRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(FlashDealDto model)
        {
            var data = _mapper.Map<Flash_deal>(model);
            try
            {
                _flashDealRepository.Add(data);
                await _flashDealRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create flash deal successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _flashDealRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _flashDealRepository.Remove(data);
                    await _flashDealRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete flash deal successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete flash deal failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<FlashDealDto> GetById(int id)
        {
            return await _flashDealRepository.FindAll(x => x.ID == id)
                                             .ProjectTo<FlashDealDto>(_configuration)
                                             .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<FlashDealDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _flashDealRepository.FindAll().ProjectTo<FlashDealDto>(_configuration)
                                                     .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Title.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<FlashDealDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(FlashDealDto model)
        {
            var data = _mapper.Map<Flash_deal>(model);
            try
            {
                _flashDealRepository.Update(data);
                await _flashDealRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create flash deal successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}