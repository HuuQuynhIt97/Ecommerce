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
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public TicketService(
            ITicketRepository ticketRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(TicketDto model)
        {
            var data = _mapper.Map<Ticket>(model);
            try
            {
                _ticketRepository.Add(data);
                await _ticketRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create ticket successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int userId)
        {
            var data = await _ticketRepository.FindAll(x => x.ID == id && x.User_ID == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _ticketRepository.Remove(data);
                    await _ticketRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete ticket successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete ticket failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<TicketDto> GetById(int id, int userId)
        {
            return await _ticketRepository.FindAll(x => x.ID == id && x.User_ID == userId)
                                          .ProjectTo<TicketDto>(_configuration)
                                          .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<TicketDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _ticketRepository.FindAll().ProjectTo<TicketDto>(_configuration).OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text) ||
                                       x.Code.Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<TicketDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(TicketDto model)
        {
            var data = _mapper.Map<Ticket>(model);
            try
            {
                _ticketRepository.Update(data);
                await _ticketRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update ticket successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}