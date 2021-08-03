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
    public class SellerWithdrawRequestService : ISellerWithdrawRequestService
    {
        private readonly ISellerWithdrawRequestRepository _sellerWithdrawRequestRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public SellerWithdrawRequestService(
            ISellerWithdrawRequestRepository sellerWithdrawRequestRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _sellerWithdrawRequestRepository = sellerWithdrawRequestRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(SellerWithdrawRequestDto model)
        {
            var data = _mapper.Map<Seller_withdraw_request>(model);
            try
            {
                _sellerWithdrawRequestRepository.Add(data);
                await _sellerWithdrawRequestRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create seller withdraw request successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            var data = await _sellerWithdrawRequestRepository.FindAll(x => x.ID == id && x.User_ID == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _sellerWithdrawRequestRepository.Add(data);
                    await _sellerWithdrawRequestRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete seller withdraw request successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete seller withdraw request failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<SellerWithdrawRequestDto> GetById(int id, int userId)
        {
            return await _sellerWithdrawRequestRepository.FindAll(x => x.ID == id && x.User_ID == userId)
                                                         .ProjectTo<SellerWithdrawRequestDto>(_configuration)
                                                         .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<SellerWithdrawRequestDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _sellerWithdrawRequestRepository.FindAll().ProjectTo<SellerWithdrawRequestDto>(_configuration)
                                                                 .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<SellerWithdrawRequestDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(SellerWithdrawRequestDto model)
        {
            var data = _mapper.Map<Seller_withdraw_request>(model);
            try
            {
                _sellerWithdrawRequestRepository.Update(data);
                await _sellerWithdrawRequestRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update seller withdraw request successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}