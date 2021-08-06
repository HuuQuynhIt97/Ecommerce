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
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public WalletService(
            IWalletRepository walletRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(WalletDto model)
        {
            var data = _mapper.Map<Wallet>(model);
            try
            {
                _walletRepository.Add(data);
                await _walletRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create wallet successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            var data = await _walletRepository.FindAll(x => x.ID == id && x.User_ID == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _walletRepository.Remove(data);
                    await _walletRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete wallet successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete wallet failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<WalletDto> GetById(int id, int userId)
        {
            return await _walletRepository.FindAll(x => x.ID == id && x.User_ID == userId)
                                          .ProjectTo<WalletDto>(_configuration)
                                          .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<WalletDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _walletRepository.FindAll().ProjectTo<WalletDto>(_configuration).OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<WalletDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(WalletDto model)
        {
            var data = _mapper.Map<Wallet>(model);
            try
            {
                _walletRepository.Update(data);
                await _walletRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update wallet successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}