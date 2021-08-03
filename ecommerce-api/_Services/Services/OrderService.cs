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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public OrderService(
            IOrderRepository orderRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(OrderDto model)
        {
            var data = _mapper.Map<Order>(model);
            try
            {
                _orderRepository.Add(data);
                await _orderRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create order successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId, int guestId, int sellerId)
        {
            var data = await _orderRepository.FindAll(x => x.ID == id && 
                                                           x.User_ID == userId && 
                                                           x.Guest_ID == guestId &&
                                                           x.Seller_ID == sellerId)
                                             .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _orderRepository.Remove(data);
                    await _orderRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete order successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
                return await Task.FromResult(operationResult);
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete order failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<OrderDto> GetById(int id, int userId, int guestId, int sellerId)
        {
            return await _orderRepository.FindAll(x => x.ID == id && 
                                                       x.User_ID == userId && 
                                                       x.Guest_ID == guestId &&
                                                       x.Seller_ID == sellerId)
                                         .ProjectTo<OrderDto>(_configuration)
                                         .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<OrderDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _orderRepository.FindAll().ProjectTo<OrderDto>(_configuration)
                                                 .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Guest_ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Seller_ID.ToString().Contains(text) ||
                                       x.Code.ToString().Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<OrderDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(OrderDto model)
        {
            var data = _mapper.Map<Order>(model);
            try
            {
                _orderRepository.Update(data);
                await _orderRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update order successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}