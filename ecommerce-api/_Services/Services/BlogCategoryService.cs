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
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public BlogCategoryService(
            IBlogCategoryRepository blogCategoryRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(Blog_categoryDto model)
        {
            var data = _mapper.Map<Blog_category>(model);
            try
            {
                _blogCategoryRepository.Add(data);
                await _blogCategoryRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create blog category successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = _blogCategoryRepository.FindAll(x => x.ID == id).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    _blogCategoryRepository.Remove(data);
                    await _blogCategoryRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete blog category successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
                return await Task.FromResult(operationResult);
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete blog category failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PageListUtility<Blog_categoryDto>> GetDataWithPaination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _blogCategoryRepository.FindAll().ProjectTo<Blog_categoryDto>(_configuration)
                                                        .OrderByDescending(x => x.Created_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Category_name.Contains(text))
                            .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<Blog_categoryDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(Blog_categoryDto model)
        {
            var data = _mapper.Map<Blog_category>(model);
            try
            {
                _blogCategoryRepository.Update(data);
                await _blogCategoryRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update blog category successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}