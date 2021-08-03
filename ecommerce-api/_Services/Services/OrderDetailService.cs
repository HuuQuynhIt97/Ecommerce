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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public OrderDetailService(
            IOrderDetailRepository orderDetailRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(OrderDetailDto model)
        {
            var data = _mapper.Map<Order_detail>(model);
            try
            {
                _orderDetailRepository.Add(data);
                await _orderDetailRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create order detail successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int orderId, int sellerId, int productId)
        {
            var data = await _orderDetailRepository.FindAll(x => x.ID == id && 
                                                           x.Order_ID == orderId && 
                                                           x.Product_ID == productId &&
                                                           x.Seller_ID == sellerId)
                                                    .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _orderDetailRepository.Remove(data);
                    await _orderDetailRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete order detail successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
                return await Task.FromResult(operationResult);
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete order detail failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<OrderDetailDto> GetById(int id, int orderId, int sellerId, int productId)
        {
            return await _orderDetailRepository.FindAll(x => x.ID == id && 
                                                       x.Order_ID == orderId && 
                                                       x.Product_ID == productId &&
                                                       x.Seller_ID == sellerId)
                                                .ProjectTo<OrderDetailDto>(_configuration)
                                                .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<OrderDetailDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _orderDetailRepository.FindAll().ProjectTo<OrderDetailDto>(_configuration)
                                                       .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Order_ID.ToString().Contains(text) ||
                                       x.Product_ID.ToString().Contains(text) ||
                                       x.Seller_ID.ToString().Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<OrderDetailDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(OrderDetailDto model)
        {
            var data = _mapper.Map<Order_detail>(model);
            try
            {
                _orderDetailRepository.Update(data);
                await _orderDetailRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update order detail successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}