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
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private OperationResult operationResult;

        public ConversationService(
            IConversationRepository conversationRepository, 
            IMapper mapper, 
            MapperConfiguration configuration)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OperationResult> Create(ConversationDto model)
        {
            var data = _mapper.Map<Conversation>(model);
            try
            {
                _conversationRepository.Add(data);
                await _conversationRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create conversation successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }

        public async Task<OperationResult> Delete(int id, int senderId, int receiverId)
        {
            var data = await _conversationRepository.FindAll(x => x.ID == id &&
                                                                  x.Sender_ID == senderId &&
                                                                  x.Receiver_ID == senderId)
                                                    .FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    _conversationRepository.Remove(data);
                    await _conversationRepository.SaveAll();
                    operationResult = new OperationResult { Success = true, Message = "Delete conversation successfully" };
                }
                catch (System.Exception ex)
                {
                    operationResult = new OperationResult { Success = false, Message = ex.ToString() };
                }
            }
            else
                operationResult = new OperationResult { Success = false, Message = "Delete conversation failes" };
            return await Task.FromResult(operationResult);
        }

        public async Task<ConversationDto> GetById(int id, int senderId, int receiverId)
        {
            return await _conversationRepository.FindAll(x => x.ID == id &&
                                                              x.Sender_ID == senderId &&
                                                              x.Receiver_ID == receiverId)
                                                .ProjectTo<ConversationDto>(_configuration)
                                                .FirstOrDefaultAsync();
        }

        public async Task<PageListUtility<ConversationDto>> GetDataWithPagination(PaginationParams param, string text, bool isPaging = true)
        {
            var data = _conversationRepository.FindAll().ProjectTo<ConversationDto>(_configuration)
                                                        .OrderByDescending(x => x.Updated_at);
            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(x => x.ID.ToString().Contains(text) ||
                                       x.Sender_ID.ToString().Contains(text) ||
                                       x.Receiver_ID.ToString().Contains(text) ||
                                       x.Title.Contains(text))
                           .OrderByDescending(x => x.Updated_at);
            }
            return await PageListUtility<ConversationDto>.PageListAsync(data, param.PageNumber, param.PageSize, isPaging);
        }

        public async Task<OperationResult> Update(ConversationDto model)
        {
            var data = _mapper.Map<Conversation>(model);
            try
            {
                _conversationRepository.Update(data);
                await _conversationRepository.SaveAll();
                operationResult = new OperationResult { Success = true, Message = "Create conversation successfully" };
            }
            catch (System.Exception ex)
            {
                operationResult = new OperationResult { Success = false, Message = ex.ToString() };
            }
            return await Task.FromResult(operationResult);
        }
    }
}