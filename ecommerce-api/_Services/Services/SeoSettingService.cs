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
    public class SeoSettingService : ISeoSettingService
    {
        private readonly ISeoSettingRepository _seoSettingRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public SeoSettingService(
            ISeoSettingRepository seoSettingRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _seoSettingRepository = seoSettingRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(SeoSettingDto model)
        {
            var data = _mapper.Map<Seo_setting>(model);
            try
            {
                _seoSettingRepository.Add(data);
                await _seoSettingRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create seo setting successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _seoSettingRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _seoSettingRepository.Remove(data);
                    await _seoSettingRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete seo setting successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete seo setting failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<SeoSettingDto> GetById(int id)
        {
            return await _seoSettingRepository.FindAll(x => x.ID == id)
                                              .ProjectTo<SeoSettingDto>(_configuration)
                                              .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<SeoSettingDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _seoSettingRepository.FindAll().ProjectTo<SeoSettingDto>(_configuration)
                                                      .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Keyword.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<SeoSettingDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(SeoSettingDto model)
        {
            var data = _mapper.Map<Seo_setting>(model);
            try
            {
                _seoSettingRepository.Update(data);
                await _seoSettingRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create seo setting successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}