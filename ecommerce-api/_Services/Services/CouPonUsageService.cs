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
    public class CouPonUsageService : ICouPonUsageService
    {
        private readonly ICouponUsageRepository _couponUsageRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CouPonUsageService(
            ICouponUsageRepository couponUsageRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _couponUsageRepository = couponUsageRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CouponUsageDto model)
        {
            var data = _mapper.Map<Coupon_usages>(model);
            try
            {
                _couponUsageRepository.Add(data);
                await _couponUsageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create coupon usage successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId, int couponId)
        {
            var data = await _couponUsageRepository.FindAll(x => x.ID == id &&
                                                                 x.User_ID == userId &&
                                                                 x.Coupon_ID == couponId)
                                                    .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _couponUsageRepository.Remove(data);
                    await _couponUsageRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete coupon usage successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete coupon usage failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CouponUsageDto> GetById(int id, int userId, int couponId)
        {
            return await _couponUsageRepository.FindAll(x => x.ID == id &&
                                                             x.User_ID == userId &&
                                                             x.Coupon_ID == couponId)
                                                .ProjectTo<CouponUsageDto>(_configuration)
                                                .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CouponUsageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _couponUsageRepository.FindAll().ProjectTo<CouponUsageDto>(_configuration)
                                                        .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Coupon_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CouponUsageDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CouponUsageDto model)
        {
            var data = _mapper.Map<Coupon_usages>(model);
            try
            {
                _couponUsageRepository.Update(data);
                await _couponUsageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create coupon usage successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}