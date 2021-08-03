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
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public LinkService(
            ILinkRepository linkRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _linkRepository = linkRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(LinkDto model)
        {
            var data = _mapper.Map<Link>(model);
            try
            {
                _linkRepository.Add(data);
                await _linkRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create link successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = await _linkRepository.FindAll(x => x.ID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _linkRepository.Remove(data);
                    await _linkRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete link successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete link failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<LinkDto> GetById(int id)
        {
            return await _linkRepository.FindAll(x => x.ID == id)
                                        .ProjectTo<LinkDto>(_configuration)
                                        .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<LinkDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _linkRepository.FindAll().ProjectTo<LinkDto>(_configuration)
                                                .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<LinkDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(LinkDto model)
        {
            var data = _mapper.Map<Link>(model);
            try
            {
                _linkRepository.Update(data);
                await _linkRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create link successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}