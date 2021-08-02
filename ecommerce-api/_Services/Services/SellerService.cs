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
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;
        public SellerService(ISellerRepository sellerRepository, IMapper mapper, MapperConfiguration configuration)
        {
            _sellerRepository = sellerRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(SellerDto model)
        {
            var data = _mapper.Map<Seller>(model);
            try
            {
                _sellerRepository.Add(data);
                await _sellerRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create seller successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            var data = await _sellerRepository.FindAll(x => x.ID == id && x.User_ID == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _sellerRepository.Add(data);
                    await _sellerRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete seller successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete seller failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<SellerDto> GetById(int id, int userId)
        {
            return await _sellerRepository.FindAll(x => x.ID == id && x.User_ID == userId)
                                          .ProjectTo<SellerDto>(_configuration)
                                          .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<SellerDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _sellerRepository.FindAll().ProjectTo<SellerDto>(_configuration).OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Verification_info.Contains(text))
                            .OrderByDescending(x => x.Created_at);
            }
            return await PageListUtility<SellerDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(SellerDto model)
        {
            var data = _mapper.Map<Seller>(model);
            try
            {
                _sellerRepository.Update(data);
                await _sellerRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update seller successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}