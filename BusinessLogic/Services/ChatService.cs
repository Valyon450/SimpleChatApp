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

namespace BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly ISimpleChatDbContext _context;
        private readonly IMapper _mapper;
        private readonly IChatValidationService _validationService;
        private readonly ILogger<ChatService> _logger;

        public ChatService(ISimpleChatDbContext context, IMapper mapper, IChatValidationService validationService, ILogger<ChatService> logger)
        {
            _context = context;
            _mapper = mapper;
            _validationService = validationService;
            _logger = logger;
        }

        public async Task<IEnumerable<ChatDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var chats = await _context.Chat.ToListAsync(cancellationToken);
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
                var chat = await _context.Chat.FindAsync(new object[] { id }, cancellationToken);

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

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {requestObject.Id} not found.");
                }

                // Map the updated properties from the request object to the existing chat
                _mapper.Map(requestObject, chat);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Chat with Id: {chat.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating chat with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var chat = await _context.Chat.FindAsync(new object[] { id }, cancellationToken);

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {id} not found.");
                }

                _context.Chat.Remove(chat);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Chat with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting chat with Id: {id}.");
                throw;
            }
        }

        public async Task AddUserToChatRequestAsync(AddUserToChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var chat = await _context.Chat.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {requestObject.Id} not found.");
                }

                // TODO: Implement Adding

                _logger.LogInformation($"User with Id: {requestObject.UserId} has been added successfully to the chat with Id: {requestObject.Id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding user with Id: {requestObject.UserId} to the chat with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task RemoveUserFromChatRequestAsync(RemoveUserFromChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var chat = await _context.Chat.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (chat == null)
                {
                    throw new Exception($"Chat with Id: {requestObject.Id} not found.");
                }

                // TODO: Implement Removing

                _logger.LogInformation($"User with Id: {requestObject.UserId} has been removed successfully from the chat with Id: {requestObject.Id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while removing user with Id: {requestObject.UserId} from the chat with Id: {requestObject.Id}.");
                throw;
            }
        }
    }
}
