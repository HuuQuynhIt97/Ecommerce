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
    public class CustomerPackagePaymentService : ICustomerPackagePaymentService
    {
        private readonly ICustomerPackagePaymentRepository _customerPackagePaymentRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CustomerPackagePaymentService(
            ICustomerPackagePaymentRepository customerPackagePaymentRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _customerPackagePaymentRepository = customerPackagePaymentRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CustomerPackagePaymentDto model)
        {
            var data = _mapper.Map<Customer_package_payment>(model);
            try
            {
                _customerPackagePaymentRepository.Add(data);
                await _customerPackagePaymentRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create customer package payment successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId, int customerPackageId)
        {
            var data = await _customerPackagePaymentRepository.FindAll(x => x.ID == id &&
                                                                       x.User_ID == userId &&
                                                                       x.Customer_package_ID == customerPackageId)
                                                    .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _customerPackagePaymentRepository.Remove(data);
                    await _customerPackagePaymentRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete customer package payment successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete customer package payment failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CustomerPackagePaymentDto> GetById(int id, int userId, int customerPackageId)
        {
            return await _customerPackagePaymentRepository.FindAll(x => x.ID == id &&
                                                                   x.User_ID == userId &&
                                                                   x.Customer_package_ID == customerPackageId)
                                                .ProjectTo<CustomerPackagePaymentDto>(_configuration)
                                                .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CustomerPackagePaymentDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _customerPackagePaymentRepository.FindAll().ProjectTo<CustomerPackagePaymentDto>(_configuration)
                                                        .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Customer_package_ID.ToString().Contains(text) ||
                                       x.Payment_detail.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CustomerPackagePaymentDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CustomerPackagePaymentDto model)
        {
            var data = _mapper.Map<Customer_package_payment>(model);
            try
            {
                _customerPackagePaymentRepository.Update(data);
                await _customerPackagePaymentRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create customer package payment successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}