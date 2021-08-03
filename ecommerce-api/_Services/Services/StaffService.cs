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
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public StaffService(
            IStaffRepository staffRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(StaffDto model)
        {
            var data = _mapper.Map<Staff>(model);
            try
            {
                _staffRepository.Add(data);
                await _staffRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create staff successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId, int roleId)
        {
            var data = await _staffRepository.FindAll(x => x.ID == id &&
                                                           x.User_ID == userId &&
                                                           x.Role_ID == roleId)
                                             .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _staffRepository.Update(data);
                    await _staffRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete staff successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete staff failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<StaffDto> GetById(int id, int userId, int roleId)
        {
            return await _staffRepository.FindAll(x => x.ID == id &&
                                                       x.User_ID == userId &&
                                                       x.Role_ID == roleId)
                                         .ProjectTo<StaffDto>(_configuration)
                                         .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<StaffDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _staffRepository.FindAll().ProjectTo<StaffDto>(_configuration)
                                                 .OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Role_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<StaffDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(StaffDto model)
        {
            var data = _mapper.Map<Staff>(model);
            try
            {
                _staffRepository.Update(data);
                await _staffRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update staff successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}