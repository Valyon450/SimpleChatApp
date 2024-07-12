using BusinessLogic.Requests.Message;
using BusinessLogic.Validation.Services.Interfaces;
using DataAccess.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Validation.Services
{
    public class MessageValidationService : IMessageValidationService
    {
        private readonly ISimpleChatDbContext _context;
        private readonly IValidator<CreateMessageRequest> _createValidator;
        private readonly IValidator<UpdateMessageRequest> _updateValidator;

        public MessageValidationService(
            ISimpleChatDbContext context,
            IValidator<CreateMessageRequest> createValidator,
            IValidator<UpdateMessageRequest> updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ValidationResult> ValidateAsync(CreateMessageRequest requestObject)
        {
            var validationResult = await _createValidator.ValidateAsync(requestObject);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }            

            if (!await ChatIdExists(requestObject.ChatId))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.ChatId), "Chat Id does not exist."));
            }

            if (!await UserIdExists(requestObject.UserId))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.UserId), "User Id does not exist."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateAsync(UpdateMessageRequest requestObject)
        {
            var validationResult = await _updateValidator.ValidateAsync(requestObject);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!await MessageIdExists(requestObject.Id))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.Id), "Message Id does not exist."));
            }

            return validationResult;
        }              

        private async Task<bool> ChatIdExists(int id)
        {
            return await _context.Chat.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> UserIdExists(int id)
        {
            return await _context.User.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> MessageIdExists(int id)
        {
            return await _context.Message.AnyAsync(e => e.Id == id);
        }
    }
}
