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
    public class ProductTaxeService : IProductTaxeService
    {
        private readonly IProductTaxeRepository _productTaxeRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public ProductTaxeService(
            IProductTaxeRepository productTaxeRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _productTaxeRepository = productTaxeRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(ProductTaxeDto model)
        {
            var data = _mapper.Map<Product_taxe>(model);
            try
            {
                _productTaxeRepository.Add(data);
                await _productTaxeRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create product taxe successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int productId, int taxeId)
        {
            var data = await _productTaxeRepository.FindAll(x => x.ID == id &&
                                                                 x.Product_ID == productId &&
                                                                 x.Tax_ID == taxeId)
                                                    .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _productTaxeRepository.Update(data);
                    await _productTaxeRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete product taxe successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete product taxe failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<ProductTaxeDto> GetById(int id, int productId, int taxeId)
        {
            return await _productTaxeRepository.FindAll(x => x.ID == id &&
                                                             x.Product_ID == productId &&
                                                             x.Tax_ID == taxeId)
                                               .ProjectTo<ProductTaxeDto>(_configuration)
                                               .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<ProductTaxeDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _productTaxeRepository.FindAll().ProjectTo<ProductTaxeDto>(_configuration)
                                                       .OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Tax_ID.ToString().Contains(text) ||
                                       x.Product_ID.ToString().Contains(text) ||
                                       x.Tax_type.Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<ProductTaxeDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(ProductTaxeDto model)
        {
            var data = _mapper.Map<Product_taxe>(model);
            try
            {
                _productTaxeRepository.Update(data);
                await _productTaxeRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update product taxe successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}