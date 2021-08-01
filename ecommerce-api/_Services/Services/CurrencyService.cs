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
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencieRepository _currencyRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CurrencyService(
            ICurrencieRepository currencyRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CurrencyDto model)
        {
            var data = _mapper.Map<Currency>(model);
            try
            {
                _currencyRepository.Add(data);
                await _currencyRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create currency successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _currencyRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _currencyRepository.Remove(data);
                    await _currencyRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete currency successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete currency failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CurrencyDto> GetById(int id)
        {
            return await _currencyRepository.FindAll(x => x.ID == id)
                                            .ProjectTo<CurrencyDto>(_configuration)
                                            .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CurrencyDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _currencyRepository.FindAll().ProjectTo<CurrencyDto>(_configuration)
                                                    .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text) ||
                                       x.Code.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CurrencyDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CurrencyDto model)
        {
            var data = _mapper.Map<Currency>(model);
            try
            {
                _currencyRepository.Update(data);
                await _currencyRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create currency successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}