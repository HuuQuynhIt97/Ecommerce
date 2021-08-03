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
    public class HomeCategoryService : IHomeCategoryService
    {
        private readonly IHomeCategorieRepository _homeCategoryRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public HomeCategoryService(
            IHomeCategorieRepository homeCategoryRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _homeCategoryRepository = homeCategoryRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(HomeCategoryDto model)
        {
            var data = _mapper.Map<Home_category>(model);
            try
            {
                _homeCategoryRepository.Add(data);
                await _homeCategoryRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create home category successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _homeCategoryRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _homeCategoryRepository.Remove(data);
                    await _homeCategoryRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete home category successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete home category failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<HomeCategoryDto> GetById(int id)
        {
            return await _homeCategoryRepository.FindAll(x => x.ID == id)
                                                .ProjectTo<HomeCategoryDto>(_configuration)
                                                .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<HomeCategoryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _homeCategoryRepository.FindAll().ProjectTo<HomeCategoryDto>(_configuration)
                                                        .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Category_ID.ToString().Contains(text) ||
                                       x.Subsubcategories.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<HomeCategoryDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(HomeCategoryDto model)
        {
            var data = _mapper.Map<Home_category>(model);
            try
            {
                _homeCategoryRepository.Update(data);
                await _homeCategoryRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create home category successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}