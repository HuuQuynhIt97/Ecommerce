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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public SliderService(
            ISliderRepository sliderRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(SliderDto model)
        {
            var data = _mapper.Map<Slider>(model);
            try
            {
                _sliderRepository.Add(data);
                await _sliderRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create slider successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _sliderRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _sliderRepository.Remove(data);
                    await _sliderRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete slider successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete slider failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<SliderDto> GetById(int id)
        {
            return await _sliderRepository.FindAll(x => x.ID == id)
                                          .ProjectTo<SliderDto>(_configuration)
                                          .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<SliderDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _sliderRepository.FindAll().ProjectTo<SliderDto>(_configuration)
                                                  .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<SliderDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(SliderDto model)
        {
            var data = _mapper.Map<Slider>(model);
            try
            {
                _sliderRepository.Update(data);
                await _sliderRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create slider successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}