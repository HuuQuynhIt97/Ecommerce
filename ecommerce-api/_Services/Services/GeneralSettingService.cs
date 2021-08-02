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
    public class GeneralSettingService : IGeneralSettingService
    {
        private readonly IGeneralSettingRepository _generalSettingRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public GeneralSettingService(
            IGeneralSettingRepository generalSettingRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _generalSettingRepository = generalSettingRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(GeneralSettingDto model)
        {
            var data = _mapper.Map<General_setting>(model);
            try
            {
                _generalSettingRepository.Add(data);
                await _generalSettingRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create general setting successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _generalSettingRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _generalSettingRepository.Remove(data);
                    await _generalSettingRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete general setting successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete general setting failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<GeneralSettingDto> GetById(int id)
        {
            return await _generalSettingRepository.FindAll(x => x.ID == id)
                                                  .ProjectTo<GeneralSettingDto>(_configuration)
                                                  .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<GeneralSettingDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _generalSettingRepository.FindAll().ProjectTo<GeneralSettingDto>(_configuration)
                                                          .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Email.Contains(text) ||
                                       x.Address.Contains(text) ||
                                       x.Phone.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<GeneralSettingDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(GeneralSettingDto model)
        {
            var data = _mapper.Map<General_setting>(model);
            try
            {
                _generalSettingRepository.Update(data);
                await _generalSettingRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create general setting successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}