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
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CouponService(
            ICouponRepository couponRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CouponDto model)
        {
            var data = _mapper.Map<Coupon>(model);
            try
            {
                _couponRepository.Add(data);
                await _couponRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create coupon successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _couponRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _couponRepository.Remove(data);
                    await _couponRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete coupon successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete coupon failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CouponDto> GetById(int id)
        {
            return await _couponRepository.FindAll(x => x.ID == id)
                                          .ProjectTo<CouponDto>(_configuration)
                                          .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CouponDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _couponRepository.FindAll().ProjectTo<CouponDto>(_configuration)
                                                  .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Type.Contains(text) ||
                                       x.Code.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CouponDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CouponDto model)
        {
            var data = _mapper.Map<Coupon>(model);
            try
            {
                _couponRepository.Update(data);
                await _couponRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create coupon successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}