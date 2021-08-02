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
    public class CustomerPackageService : ICustomerPackageService
    {
        private readonly ICustomerPackageRepository _customerPackageRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CustomerPackageService(
            ICustomerPackageRepository customerPackageRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _customerPackageRepository = customerPackageRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CustomerPackageDto model)
        {
            var data = _mapper.Map<Customer_package>(model);
            try
            {
                _customerPackageRepository.Add(data);
                await _customerPackageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create customer package successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _customerPackageRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _customerPackageRepository.Remove(data);
                    await _customerPackageRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete customer package successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete customer package failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CustomerPackageDto> GetById(int id)
        {
            return await _customerPackageRepository.FindAll(x => x.ID == id)
                                            .ProjectTo<CustomerPackageDto>(_configuration)
                                            .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CustomerPackageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _customerPackageRepository.FindAll().ProjectTo<CustomerPackageDto>(_configuration)
                                                    .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CustomerPackageDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CustomerPackageDto model)
        {
            var data = _mapper.Map<Customer_package>(model);
            try
            {
                _customerPackageRepository.Update(data);
                await _customerPackageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create customer package successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}