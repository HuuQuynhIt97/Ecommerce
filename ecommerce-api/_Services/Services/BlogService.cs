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
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configration;
        private OperationResult operationResult;

        public BlogService(
            IBlogRepository blogRepository,
            IMapper mapper,
            MapperConfiguration configration)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _configration = configration;
        }

        public async Task<OperationResult> Create(BlogDto model)
        {
            var data = _mapper.Map<Blog>(model);
            try
            {
                _blogRepository.Add(data);
                await _blogRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create blog successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int cateId)
        {
            var data = _blogRepository.FindAll(x => x.ID == id && x.Category_ID == cateId).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    _blogRepository.Remove(data);
                    await _blogRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete blog successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete blog failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PageListUtility<BlogDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _blogRepository.FindAll().ProjectTo<BlogDto>(_configration).OrderByDescending(x => x.Created_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Category_ID.ToString().Contains(text) ||
                                       x.Title.Contains(text))
                            .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<BlogDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(BlogDto model)
        {
            var data = _mapper.Map<Blog>(model);
            try
            {
                _blogRepository.Update(data);
                await _blogRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update blog successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}