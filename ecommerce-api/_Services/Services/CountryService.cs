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
    public class CountryService : ICountryService
    {
        private readonly ICountrieRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CountryService(
            ICountrieRepository countryRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CountryDto model)
        {
            var data = _mapper.Map<Country>(model);
            try
            {
                _countryRepository.Add(data);
                await _countryRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create country successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _countryRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _countryRepository.Remove(data);
                    await _countryRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete country successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
                return await Task.FromResult(operationResult);
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete country failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CountryDto> GetById(int id)
        {
            return await _countryRepository.FindAll(x => x.ID == id).ProjectTo<CountryDto>(_configuration).FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CountryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _countryRepository.FindAll(x => x.Status == true).ProjectTo<CountryDto>(_configuration)
                                                   .OrderBy(x => x.Name);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text) ||
                                       x.Code.Contains(text))
                            .OrderBy(x => x.Name);
            }
            return await PageListUtility<CountryDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CountryDto model)
        {
            var data = _mapper.Map<Country>(model);
            try
            {
                _countryRepository.Update(data);
                await _countryRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update country successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}