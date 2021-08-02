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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CategoryDto model)
        {
            var data = _mapper.Map<Category>(model);
            try
            {
                _categoryRepository.Add(data);
                await _categoryRepository.SaveAll();
                operationResult = new OperationResult{ Success = true, Message = "Create Category successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult{ Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<PageListUtility<CategoryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _categoryRepository.FindAll().ProjectTo<CategoryDto>(_configuration).OrderByDescending(x => x.Created_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) || 
                                       x.Name.Contains(text) ||
                                       x.Parent_ID.ToString().Contains(text))
                            .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<CategoryDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CategoryDto model)
        {
            var data = _mapper.Map<Category>(model);
            try
            {
                _categoryRepository.Update(data);
                await _categoryRepository.SaveAll();
                operationResult = new OperationResult{ Success = true, Message = "Update Category successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult{ Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}