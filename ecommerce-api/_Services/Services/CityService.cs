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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CityService(
            ICityRepository cityRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CityDto model)
        {
            var data = _mapper.Map<City>(model);
            try
            {
                _cityRepository.Add(data);
                await _cityRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create city successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int countryId)
        {
            var data = await _cityRepository.FindAll(x => x.ID == id && x.Country_ID == countryId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _cityRepository.Remove(data);
                    await _cityRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete city successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete city failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CityDto> GetByID(int id, int countryId)
        {
            return await _cityRepository.FindAll(x => x.ID == id && x.Country_ID == countryId)
                                        .ProjectTo<CityDto>(_configuration)
                                        .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CityDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _cityRepository.FindAll().ProjectTo<CityDto>(_configuration)
                                                .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Country_ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CityDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CityDto model)
        {
            var data = _mapper.Map<City>(model);
            try
            {
                _cityRepository.Update(data);
                await _cityRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update city successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}