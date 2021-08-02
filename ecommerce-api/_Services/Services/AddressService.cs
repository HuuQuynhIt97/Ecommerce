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
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public AddressService(
            IAddressRepository addressRepository,
            IMapper mapper,
            MapperConfiguration configuration)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(AddressesDto model)
        {
            var data = _mapper.Map<Addresses>(model);
            try
            {
                _addressRepository.Add(data);
                await _addressRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create address successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            var data = await _addressRepository.FindAll(x => x.ID == id && x.User_ID == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _addressRepository.Remove(data);
                    await _addressRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete address successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete address failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<AddressesDto> GetDataByID(int id, int userId)
        {
            return await _addressRepository.FindAll(x => x.ID == id && x.User_ID == userId)
                                           .ProjectTo<AddressesDto>(_configuration)
                                           .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<AddressesDto>> GetDataByUserID(PaginationParams param, int userId, bool isPaging = true)
        {
            var data = _addressRepository.FindAll(x => x.User_ID == userId).ProjectTo<AddressesDto>(_configuration)
                                         .OrderByDescending(x => x.Created_at);
            return await PageListUtility<AddressesDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<PageListUtility<AddressesDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _addressRepository.FindAll().ProjectTo<AddressesDto>(_configuration)
                                                   .OrderByDescending(x => x.Created_at).ThenBy(x => x.User_ID);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Address.Contains(text) ||
                                       x.Country.Contains(text) ||
                                       x.City.Contains(text) ||
                                       x.Phone.Contains(text) ||
                                       x.Postal_code.Contains(text))
                            .OrderByDescending(x => x.Created_at).ThenBy(x => x.User_ID);
            }
            return await PageListUtility<AddressesDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> SetDefault(int id, int userId)
        {
            var data = await _addressRepository.FindAll(x => x.User_ID == userId).ToListAsync();
            foreach (var item in data)
            {
                item.Set_default = item.ID == id ? true : false;
            }
            try
            {
                _addressRepository.UpdateRange(data);
                await _addressRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Set address default successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Update(AddressesDto model)
        {
            var data = _mapper.Map<Addresses>(model);
            try
            {
                _addressRepository.Update(data);
                await _addressRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update address successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}