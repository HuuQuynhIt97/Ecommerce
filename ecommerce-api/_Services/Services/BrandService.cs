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

namespace ecommerce_api._Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public BrandService(
            IBrandRepository brandRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(BrandDto model)
        {
            var data = _mapper.Map<Brand>(model);
            try
            {
                _brandRepository.Add(data);
                await _brandRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create brand successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var result = _brandRepository.FindAll(x => x.ID == id).FirstOrDefault();
            if (result != null)
            {
                try
                {
                    _brandRepository.Remove(result);
                    await _brandRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete brand successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete brand failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PageListUtility<BrandDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _brandRepository.FindAll().ProjectTo<BrandDto>(_configuration).OrderByDescending(x => x.Created_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                            .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<BrandDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(BrandDto model)
        {
            var data = _mapper.Map<Brand>(model);
            try
            {
                _brandRepository.Update(data);
                await _brandRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update brand successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}