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
    public class TicketReplyService : ITicketReplyService
    {
        private readonly ITicketReplieRepository _ticketReplyRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public TicketReplyService(
            ITicketReplieRepository ticketReplyRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _ticketReplyRepository = ticketReplyRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(TicketReplyDto model)
        {
            var data = _mapper.Map<Ticket_reply>(model);
            try
            {
                _ticketReplyRepository.Add(data);
                await _ticketReplyRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create ticket reply successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int ticketId, int userId)
        {
            var data = await _ticketReplyRepository.FindAll(x => x.ID == id &&
                                                                 x.Ticket_ID == ticketId &&
                                                                 x.User_ID == userId)
                                                   .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _ticketReplyRepository.Update(data);
                    await _ticketReplyRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete ticket reply successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete ticket reply failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<TicketReplyDto> GetById(int id, int ticketId, int userId)
        {
            return await _ticketReplyRepository.FindAll(x => x.ID == id &&
                                                             x.Ticket_ID == ticketId &&
                                                             x.User_ID == userId)
                                               .ProjectTo<TicketReplyDto>(_configuration)
                                               .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<TicketReplyDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _ticketReplyRepository.FindAll().ProjectTo<TicketReplyDto>(_configuration)
                                                       .OrderByDescending(x => x.Updated_at);
            if(!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Ticket_ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<TicketReplyDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(TicketReplyDto model)
        {
            var data = _mapper.Map<Ticket_reply>(model);
            try
            {
                _ticketReplyRepository.Update(data);
                await _ticketReplyRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update ticket reply successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}