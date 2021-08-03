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
    public class PickupPointService : IPickupPointService
    {
        private readonly IPickupPointRepository _pickupPointRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public PickupPointService(
            IPickupPointRepository pickupPointRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _pickupPointRepository = pickupPointRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(PickupPointDto model)
        {
            var data = _mapper.Map<Pickup_point>(model);
            try
            {
                _pickupPointRepository.Add(data);
                await _pickupPointRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create pickup point successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int staffId)
        {
            var data = await _pickupPointRepository.FindAll(x => x.ID == id && x.Staff_ID == staffId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _pickupPointRepository.Remove(data);
                    await _pickupPointRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete pickup point successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete pickup point failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<PickupPointDto> GetById(int id, int staffId)
        {
            return await _pickupPointRepository.FindAll(x => x.ID == id && x.Staff_ID == staffId)
                                               .ProjectTo<PickupPointDto>(_configuration)
                                               .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<PickupPointDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _pickupPointRepository.FindAll().ProjectTo<PickupPointDto>(_configuration)
                                                       .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Staff_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<PickupPointDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(PickupPointDto model)
        {
            var data = _mapper.Map<Pickup_point>(model);
            try
            {
                _pickupPointRepository.Update(data);
                await _pickupPointRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update pickup point successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}