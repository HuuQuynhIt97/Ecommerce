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
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public MessageService(
            IMessageRepository messageRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(MessageDto model)
        {
            var data = _mapper.Map<Messages>(model);
            try
            {
                _messageRepository.Add(data);
                await _messageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create message successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int conversationId, int userId)
        {
            var data = await _messageRepository.FindAll(x => x.ID == id && x.Conversation_ID == conversationId && x.User_ID == userId)
                                               .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _messageRepository.Remove(data);
                    await _messageRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete message successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
                return await Task.FromResult(operationResult);
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete message failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<MessageDto> GetById(int id, int conversationId, int userId)
        {
            return await _messageRepository.FindAll(x => x.ID == id && x.Conversation_ID == conversationId && x.User_ID == userId)
                                           .ProjectTo<MessageDto>(_configuration)
                                           .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<MessageDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _messageRepository.FindAll().ProjectTo<MessageDto>(_configuration)
                                                   .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Conversation_ID.ToString().Contains(text) ||
                                       x.User_ID.ToString().Contains(text))
                            .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<MessageDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(MessageDto model)
        {
            var data = _mapper.Map<Messages>(model);
            try
            {
                _messageRepository.Update(data);
                await _messageRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Update message successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}