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
    public class PolicyService : IPolicyService
    {
        private readonly IPolicieRepository _policyRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public PolicyService(
            IPolicieRepository policyRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _policyRepository = policyRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(PolicyDto model)
        {
            var data = _mapper.Map<Policy>(model);
            try
            {
                _policyRepository.Add(data);
                await _policyRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create policy successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id)
        {
            var data = _policyRepository.FindAll(x => x.ID == id).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    _policyRepository.Remove(data);
                    await _policyRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete policy successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete policy failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PageListUtility<PolicyDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _policyRepository.FindAll().ProjectTo<PolicyDto>(_configuration).OrderByDescending(x => x.Created_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Name.Contains(text))
                            .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<PolicyDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(PolicyDto model)
        {
            var data = _mapper.Map<Policy>(model);
            try
            {
                _policyRepository.Update(data);
                await _policyRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create policy successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}