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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public PaymentService(
            IPaymentRepository paymentRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(PaymentDto model)
        {
            var data = _mapper.Map<Payment>(model);
            try
            {
                _paymentRepository.Add(data);
                await _paymentRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create payment successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int sellerId)
        {
            var data = await _paymentRepository.FindAll(x => x.ID == id && x.Seller_ID == sellerId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _paymentRepository.Remove(data);
                    await _paymentRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete payment successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete payment failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PaymentDto> GetById(int id, int sellerId)
        {
            return await _paymentRepository.FindAll(x => x.ID == id && x.Seller_ID == sellerId)
                                           .ProjectTo<PaymentDto>(_configuration)
                                           .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<PaymentDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _paymentRepository.FindAll().ProjectTo<PaymentDto>(_configuration)
                                                   .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Seller_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<PaymentDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(PaymentDto model)
        {
            var data = _mapper.Map<Payment>(model);
            try
            {
                _paymentRepository.Update(data);
                await _paymentRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update payment successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}