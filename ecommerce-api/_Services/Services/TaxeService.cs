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
    public class TaxeService : ITaxeService
    {
        private readonly ITaxRepository _taxeRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public TaxeService(
            ITaxRepository taxeRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _taxeRepository = taxeRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> ChangeStatus(int id)
        {
            var data = await _taxeRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            data.Tax_status = !data.Tax_status;
            _taxeRepository.Update(data);
            await _taxeRepository.SaveAll();
            return new OperationResult { Success = true, Message = "Change taxe status successfully" };
        }

        public async Task<OperationResult> Create(TaxeDto model)
        {
            var data = _mapper.Map<Taxe>(model);
            try
            {
                _taxeRepository.Add(data);
                await _taxeRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create taxe successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _taxeRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _taxeRepository.Remove(data);
                    await _taxeRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete taxe successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete taxe failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<TaxeDto> GetById(int id)
        {
            return await _taxeRepository.FindAll(x => x.ID == id)
                                        .ProjectTo<TaxeDto>(_configuration)
                                        .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<TaxeDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _taxeRepository.FindAll().ProjectTo<TaxeDto>(_configuration)
                                                .OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<TaxeDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(TaxeDto model)
        {
            var data = _mapper.Map<Taxe>(model);
            try
            {
                _taxeRepository.Update(data);
                await _taxeRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update taxe successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}