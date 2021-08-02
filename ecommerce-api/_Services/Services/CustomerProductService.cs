
using System.Linq;
using System.Threading.Tasks;
using API.Helpers.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Helpers;
using ecommerce_api.Helpers.Params;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api._Services.Services
{
    public class CustomerProductService : ICustomerProductService
    {
        private readonly ICustomerProductRepository _customerProductRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CustomerProductService(
            ICustomerProductRepository customerProductRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _customerProductRepository = customerProductRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CustomerProductDto model)
        {
            var data = _mapper.Map<Customer_product>(model);
            try
            {
                _customerProductRepository.Add(data);
                await _customerProductRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create customer product successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(CustomerProductParam param)
        {
            var data = await _customerProductRepository.FindAll(x => x.ID == param.id && 
                                                                     x.User_ID == param.userId && 
                                                                     x.Category_ID == param.categoryId && 
                                                                     x.Subcategory_ID == param.subCategoryId &&
                                                                     x.Subsubcategory_ID == param.subSubCategoryId &&
                                                                     x.Brand_ID == param.brandId)
                                                        .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _customerProductRepository.Remove(data);
                    await _customerProductRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete customer product successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete customer product failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CustomerProductDto> GetById(CustomerProductParam param)
        {
            return await _customerProductRepository.FindAll(x => x.ID == param.id && 
                                                                 x.User_ID == param.userId && 
                                                                 x.Category_ID == param.categoryId && 
                                                                 x.Subcategory_ID == param.subCategoryId &&
                                                                 x.Subsubcategory_ID == param.subSubCategoryId &&
                                                                 x.Brand_ID == param.brandId)
                                                    .ProjectTo<CustomerProductDto>(_configuration)
                                                    .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CustomerProductDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _customerProductRepository.FindAll().ProjectTo<CustomerProductDto>(_configuration)
                                                           .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text) ||
                                       x.Added_by.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CustomerProductDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CustomerProductDto model)
        {
            var data = _mapper.Map<Customer_product>(model);
            try
            {
                _customerProductRepository.Update(data);
                await _customerProductRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create customer product successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}