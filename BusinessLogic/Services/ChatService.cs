using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Requests.Chat;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Validation.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using BusinessLogic.Hubs;

namespace BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly ISimpleChatDbContext _context;
        private readonly IMapper _mapper;
        private readonly IChatValidationService _validationService;
        private readonly ILogger<ChatService> _logger;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(
            ISimpleChatDbContext context,
            IMapper mapper,
            IChatValidationService validationService,
            ILogger<ChatService> logger,
            IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _validationService = validationService;
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<ChatDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var chats = await _context.Chat
                    .Include(c => c.CreatedBy)
                    .ToListAsync(cancellationToken);

                return _mapper.Map<IEnumerable<ChatDTO>>(chats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching chats.");
                return null;
            }
        }

        public async Task<ChatDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var chat = await _context.Chat
                    .Include(c => c.CreatedBy)
                    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {id} not found.");
                }

                return _mapper.Map<ChatDTO>(chat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching chat with Id: {id}.");
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllChatMembersAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var chat = await _context.Chat.FindAsync(new object[] { id }, cancellationToken);

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {id} not found.");
                }

                var chatMembers = await _context.UserChat
                    .Where(uc => uc.ChatId == id)
                    .Include(uc => uc.User)
                    .Select(uc => _mapper.Map<UserDTO>(uc.User))
                    .ToListAsync(cancellationToken);

                return chatMembers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching members of chat with Id: {id}.");
                throw;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetAllChatMessagesAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var chat = await _context.Chat.FindAsync(new object[] { id }, cancellationToken);

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {id} not found.");
                }

                var chatMessages = await _context.Message
                    .Where(m => m.ChatId == id)
                    .Include(m => m.User)
                    .Include(m => m.Chat)
                    .Select(m => _mapper.Map<MessageDTO>(m))
                    .ToListAsync(cancellationToken);

                return chatMessages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching messages of chat with Id: {id}.");
                throw;
            }
        }

        public async Task<List<ChatDTO>> SearchChatsAsync(string searchQuery, CancellationToken cancellationToken)
        {
            try
            {
                var chats = await _context.Chat
                    .Include(c => c.CreatedBy)
                    .Where(c => c.Name.Contains(searchQuery))
                    .ToListAsync(cancellationToken);

                return _mapper.Map<List<ChatDTO>>(chats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for chats.");
                return null;
            }
        }

        public async Task<int> CreateAsync(CreateChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var chat = _mapper.Map<Chat>(requestObject);

                _context.Chat.Add(chat);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Chat with Id: {chat.Id} has been created successfully.");

                await _hubContext.Clients.All.SendAsync("ChatCreated", _mapper.Map<ChatDTO>(chat));

                return chat.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a chat.");
                throw;
            }
        }

        public async Task UpdateAsync(UpdateChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var chat = await _context.Chat.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                // Map the updated properties from the request object to the existing chat
                _mapper.Map(requestObject, chat);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Chat with Id: {chat.Id} has been updated successfully.");

                await _hubContext.Clients.All.SendAsync("ChatUpdated", _mapper.Map<ChatDTO>(chat));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating chat with Id: {requestObject.Id}.");
                throw;
            }
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id, int userId, CancellationToken cancellationToken)
        {
            try
            {
                var chat = await _context.Chat.FindAsync(new object[] { id }, cancellationToken);

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {id} not found.");
                }

                if(chat.CreatedById != userId)
                {
                    throw new Exception($"Denied access. Invalid chat creator Id.");
                }                

                _context.Chat.Remove(chat);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Chat with Id: {id} has been deleted successfully.");

                await _hubContext.Clients.All.SendAsync("ChatDeleted", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting chat with Id: {id}.");
                throw;
            }
        }

        public async Task AddUserToChatAsync(AddUserToChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var chat = await _context.Chat.FindAsync(new object[] { requestObject.Id }, cancellationToken);                

                var userChat = new UserChat
                {
                    UserId = requestObject.UserId,
                    ChatId = requestObject.Id
                };

                _context.UserChat.Add(userChat);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"User with Id: {requestObject.UserId} has been added successfully to the chat with Id: {requestObject.Id}.");

                await _hubContext.Clients.Group(chat.Id.ToString()).SendAsync("UserJoined", requestObject.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding user with Id: {requestObject.UserId} to the chat with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task RemoveUserFromChatAsync(RemoveUserFromChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var chat = await _context.Chat.FindAsync(new object[] { requestObject.Id }, cancellationToken);                

                var userChat = await _context.UserChat
                    .FirstOrDefaultAsync(uc => uc.ChatId == requestObject.Id && uc.UserId == requestObject.UserId, cancellationToken);

                if (userChat == null)
                {
                    throw new Exception($"User with Id: {requestObject.UserId} is not part of the chat with Id: {requestObject.Id}.");
                }

                _context.UserChat.Remove(userChat);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"User with Id: {requestObject.UserId} has been removed successfully from the chat with Id: {requestObject.Id}.");

                await _hubContext.Clients.Group(chat.Id.ToString()).SendAsync("UserLeft", requestObject.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while removing user with Id: {requestObject.UserId} from the chat with Id: {requestObject.Id}.");
                throw;
            }
        }
    }
}
