using System.Linq;
using System.Threading.Tasks;
using API.Helpers.Utilities;
using AutoMapper;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Services.Interface;
using ecommerce_api.DTO;
using ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api._Services.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public async Task<int> CountSubscribe()
        {
            var data = await _subscribeRepository.FindAll().ToListAsync();
            return data.Count();
        }

        public async Task<OperationResult> Subscribe(SubscribeDto model)
        {
            var data = _mapper.Map<Subscribe>(model);
            try
            {
                _subscribeRepository.Add(data);
                await _subscribeRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Subscribe successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}