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
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public PageService(
            IPageRepository pageRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(PageDto model)
        {
            var data = _mapper.Map<Page>(model);
            try
            {
                _pageRepository.Add(data);
                await _pageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create page successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _pageRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _pageRepository.Remove(data);
                    await _pageRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete page successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete page failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PageDto> GetById(int id)
        {
            return await _pageRepository.FindAll(x => x.ID == id)
                                        .ProjectTo<PageDto>(_configuration)
                                        .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<PageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _pageRepository.FindAll().ProjectTo<PageDto>(_configuration)
                                                .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Type.Contains(text) ||
                                       x.Title.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<PageDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(PageDto model)
        {
            var data = _mapper.Map<Page>(model);
            try
            {
                _pageRepository.Update(data);
                await _pageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create page successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}