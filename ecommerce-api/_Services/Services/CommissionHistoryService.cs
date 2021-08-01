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
    public class CommissionHistoryService : ICommissionHistoryService
    {
        private readonly ICommissionHistorieRepository _commissionHistorieRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public CommissionHistoryService(
            ICommissionHistorieRepository commissionHistorieRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _commissionHistorieRepository = commissionHistorieRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(CommissionHistoryDto model)
        {
            var data = _mapper.Map<Commission_history>(model);
            try
            {
                _commissionHistorieRepository.Add(data);
                await _commissionHistorieRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create commission history successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int orderId, int orderDetailId, int sellerId)
        {
            var data = await _commissionHistorieRepository.FindAll(x => x.ID == id &&
                                                                        x.Order_ID == orderId &&
                                                                        x.Order_detail_ID == orderDetailId &&
                                                                        x.Seller_ID == sellerId)
                                                          .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _commissionHistorieRepository.Remove(data);
                    await _commissionHistorieRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete commission history successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete commission history failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<CommissionHistoryDto> GetById(int id, int orderId, int orderDetailId, int sellerId)
        {
            return await _commissionHistorieRepository.FindAll(x => x.ID == id &&
                                                                    x.Order_ID == orderId &&
                                                                    x.Order_detail_ID == orderDetailId &&
                                                                    x.Seller_ID == sellerId)
                                                      .ProjectTo<CommissionHistoryDto>(_configuration)
                                                      .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<CommissionHistoryDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _commissionHistorieRepository.FindAll().ProjectTo<CommissionHistoryDto>(_configuration)
                                                    .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Order_ID.ToString().Contains(text) ||
                                       x.Order_detail_ID.ToString().Contains(text) ||
                                       x.Seller_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<CommissionHistoryDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(CommissionHistoryDto model)
        {
            var data = _mapper.Map<Commission_history>(model);
            try
            {
                _commissionHistorieRepository.Update(data);
                await _commissionHistorieRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update commission history successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}