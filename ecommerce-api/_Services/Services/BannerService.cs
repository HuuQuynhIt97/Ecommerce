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
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public BannerService(
            IBannerRepository bannerRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(BannerDto model)
        {
            var data = _mapper.Map<Banner>(model);
            try
            {
                _bannerRepository.Add(data);
                await _bannerRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create banner successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _bannerRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _bannerRepository.Remove(data);
                    await _bannerRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete banner successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete banner failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<BannerDto> GetById(int id)
        {
            return await _bannerRepository.FindAll(x => x.ID == id)
                                          .ProjectTo<BannerDto>(_configuration)
                                          .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<BannerDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _bannerRepository.FindAll().ProjectTo<BannerDto>(_configuration)
                                                  .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text)).OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<BannerDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(BannerDto model)
        {
            var data = _mapper.Map<Banner>(model);
            try
            {
                _bannerRepository.Update(data);
                await _bannerRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update banner successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}