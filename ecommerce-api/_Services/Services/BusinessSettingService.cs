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
    public class BusinessSettingService : IBusinessSettingService
    {
        private readonly IBusinessSettingRepository _businessSettingRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public BusinessSettingService(
            IBusinessSettingRepository businessSettingRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _businessSettingRepository = businessSettingRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(Business_settingDto model)
        {
            var data = _mapper.Map<Business_setting>(model);
            try
            {
                _businessSettingRepository.Add(data);
                await _businessSettingRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create business setting successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _businessSettingRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _businessSettingRepository.Remove(data);
                    await _businessSettingRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete business setting successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete business setting failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<Business_settingDto> GetById(int id)
        {
            return await _businessSettingRepository.FindAll(x => x.ID == id)
                                                   .ProjectTo<Business_settingDto>(_configuration)
                                                   .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<Business_settingDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _businessSettingRepository.FindAll().ProjectTo<Business_settingDto>(_configuration)
                                                           .OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Type.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<Business_settingDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(Business_settingDto model)
        {
            var data = _mapper.Map<Business_setting>(model);
            try
            {
                _businessSettingRepository.Add(data);
                await _businessSettingRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update business setting successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}