using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Requests.Message;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Validation.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISimpleChatDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageValidationService _validationService;
        private readonly ILogger<MessageService> _logger;

        public MessageService(ISimpleChatDbContext context, IMapper mapper, IMessageValidationService validationService, ILogger<MessageService> logger)
        {
            _context = context;
            _mapper = mapper;
            _validationService = validationService;
            _logger = logger;
        }

        public async Task<IEnumerable<MessageDTO>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var messages = await _context.Message.ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<MessageDTO>>(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching messages.");
                return null;
            }
        }

        public async Task<MessageDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _context.Message.FindAsync(new object[] { id }, cancellationToken);

                if (message == null)
                {
                    throw new Exception($"Message with Id: {id} not found.");
                }

                return _mapper.Map<MessageDTO>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching message with Id: {id}.");
                throw;
            }
        }

        public async Task<int> CreateAsync(CreateMessageRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var message = _mapper.Map<Message>(requestObject);

                _context.Message.Add(message);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Message with Id: {message.Id} has been created successfully.");

                return message.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a message.");
                throw;
            }
        }

        public async Task UpdateAsync(UpdateMessageRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validationService.ValidateAsync(requestObject);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                var message = await _context.Message.FindAsync(new object[] { requestObject.Id }, cancellationToken);

                if (message == null)
                {
                    throw new Exception($"Message with Id: {requestObject.Id} not found.");
                }

                // Map the updated properties from the request object to the existing message
                _mapper.Map(requestObject, message);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Message with Id: {message.Id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating message with Id: {requestObject.Id}.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _context.Message.FindAsync(new object[] { id }, cancellationToken);

                if (message == null)
                {
                    throw new Exception($"Message with Id: {id} not found.");
                }

                _context.Message.Remove(message);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Message with Id: {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting message with Id: {id}.");
                throw;
            }
        }
    }
}
