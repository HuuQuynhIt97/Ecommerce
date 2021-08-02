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
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public AttributeService(
            IAttributeRepository attributeRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _attributeRepository = attributeRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(AttributeDto model)
        {
            var data = _mapper.Map<Attributes>(model);
            try
            {
                _attributeRepository.Add(data);
                await _attributeRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create attribute successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _attributeRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _attributeRepository.Remove(data);
                    await _attributeRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete attribute successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete attribuute failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<AttributeDto> GetById(int id)
        {
            return await _attributeRepository.FindAll(x => x.ID == id)
                                             .ProjectTo<AttributeDto>(_configuration)
                                             .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<AttributeDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _attributeRepository.FindAll().ProjectTo<AttributeDto>(_configuration)
                                                     .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<AttributeDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(AttributeDto model)
        {
            var data = _mapper.Map<Attributes>(model);
            try
            {
                _attributeRepository.Update(data);
                await _attributeRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update attribute successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}