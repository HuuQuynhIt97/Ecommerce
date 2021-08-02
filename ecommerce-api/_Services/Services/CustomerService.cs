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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CustomerService(
            ICustomerRepository customerRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CustomerDto model)
        {
            var data = _mapper.Map<Customer>(model);
            try
            {
                _customerRepository.Add(data);
                await _customerRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create customer successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            var data = await _customerRepository.FindAll(x => x.ID == id && x.User_ID == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _customerRepository.Remove(data);
                    await _customerRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete customer successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
                return await Task.FromResult(operationResult);
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete customer failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CustomerDto> GetById(int id, int userId)
        {
            return await _customerRepository.FindAll(x => x.ID == id && x.User_ID == userId).ProjectTo<CustomerDto>(_configuration).FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CustomerDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _customerRepository.FindAll().ProjectTo<CustomerDto>(_configuration)
                                                    .OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CustomerDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CustomerDto model)
        {
            var data = _mapper.Map<Customer>(model);
            try
            {
                _customerRepository.Update(data);
                await _customerRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update customer successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}