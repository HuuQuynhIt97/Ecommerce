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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(ProductDto model)
        {
            var data = _mapper.Map<Product>(model);
            try
            {
                _productRepository.Add(data);
                await _productRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create Product successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int cateId, int brandId)
        {
            var product = _productRepository.FindAll(x => x.ID == id &&
                                                     x.Category_ID == cateId &&
                                                     x.Brand_ID == brandId)
                                            .FirstOrDefault();
            if (product != null)
            {
                try
                {
                    _productRepository.Remove(product);
                    await _productRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete product successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete product failes" };

            return await Task.FromResult(operationResult);
        }

        public async Task<ProductDto> GetById(int id, int cateId, int brandId)
        {
            return await _productRepository.FindAll(x => x.ID == id && x.Category_ID == cateId && x.Brand_ID == brandId)
                                           .ProjectTo<ProductDto>(_configuration).FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<ProductDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _productRepository.FindAll().ProjectTo<ProductDto>(_configuration).OrderByDescending(x => x.Created_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text) ||
                                       x.Barcode.Contains(text))
                            .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<ProductDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(ProductDto model)
        {
            var data = _mapper.Map<Product>(model);
            try
            {
                _productRepository.Update(data);
                await _productRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create Product successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}