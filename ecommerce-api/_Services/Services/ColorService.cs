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
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public ColorService(
            IColorRepository colorRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(ColorDto model)
        {
            var data = _mapper.Map<Color>(model);
            try
            {
                _colorRepository.Add(data);
                await _colorRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create color successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _colorRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _colorRepository.Remove(data);
                    await _colorRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete color successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete color failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<ColorDto> GetById(int id)
        {
            return await _colorRepository.FindAll(x => x.ID == id)
                                         .ProjectTo<ColorDto>(_configuration)
                                         .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<ColorDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _colorRepository.FindAll().ProjectTo<ColorDto>(_configuration)
                                                 .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text) ||
                                       x.Code.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<ColorDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(ColorDto model)
        {
            var data = _mapper.Map<Color>(model);
            try
            {
                _colorRepository.Update(data);
                await _colorRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update color successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}