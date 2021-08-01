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
    public class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public ProductStockService(
            IProductStockRepository productStockRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _productStockRepository = productStockRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(ProductStockDto model)
        {
            var data = _mapper.Map<Product_stock>(model);
            try
            {
                _productStockRepository.Add(data);
                await _productStockRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create product stock successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int productId)
        {
            var data = await _productStockRepository.FindAll(x => x.ID == id && x.Product_ID == productId)
                                                    .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _productStockRepository.Remove(data);
                    await _productStockRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete product stock successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete product stock failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<ProductStockDto> GetById(int id, int productId)
        {
            return await _productStockRepository.FindAll(x => x.ID == id && x.Product_ID == productId)
                                                .ProjectTo<ProductStockDto>(_configuration)
                                                .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<ProductStockDto>> GetDataWithpagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _productStockRepository.FindAll().ProjectTo<ProductStockDto>(_configuration)
                                                        .OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Product_ID.ToString().Contains(text) ||
                                       x.Variant.Contains(text) ||
                                       x.Sku.Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<ProductStockDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(ProductStockDto model)
        {
            var data = _mapper.Map<Product_stock>(model);
            try
            {
                _productStockRepository.Update(data);
                await _productStockRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update product stock successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}