using BusinessLogic.Requests.Chat;
using BusinessLogic.Validation.Services.Interfaces;
using DataAccess.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Validation.Services
{
    public class ChatValidationService : IChatValidationService
    {
        private readonly ISimpleChatDbContext _context;
        private readonly IValidator<CreateChatRequest> _createValidator;
        private readonly IValidator<UpdateChatRequest> _updateValidator;
        private readonly IValidator<AddUserToChatRequest> _addUserToChatValidator;
        private readonly IValidator<RemoveUserFromChatRequest> _removeUserFromChatValidator;

        public ChatValidationService(
            ISimpleChatDbContext context,
            IValidator<CreateChatRequest> createValidator,
            IValidator<UpdateChatRequest> updateValidator,
            IValidator<AddUserToChatRequest> addUserToChatValidator,
            IValidator<RemoveUserFromChatRequest> removeUserFromChatValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _addUserToChatValidator = addUserToChatValidator;
            _removeUserFromChatValidator = removeUserFromChatValidator;
        }
 
        public async Task<ValidationResult> ValidateAsync(CreateChatRequest requestObject)
        {
            var validationResult = await _createValidator.ValidateAsync(requestObject);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!await UserIdExists(requestObject.CreatedById))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.CreatedById), "Creator Id does not exist."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateAsync(UpdateChatRequest requestObject)
        {
            var validationResult = await _updateValidator.ValidateAsync(requestObject);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!await ChatIdExists(requestObject.Id))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.Id), "Chat Id does not exist."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateAsync(AddUserToChatRequest requestObject)
        {
            var validationResult = await _addUserToChatValidator.ValidateAsync(requestObject);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!await ChatIdExists(requestObject.Id))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.Id), "Chat Id does not exist."));
            }

            if (!await UserIdExists(requestObject.UserId))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.UserId), "UserId does not exist."));
            }

            if (await IsUserAlreadyMemberAsync(requestObject.Id, requestObject.UserId))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.UserId), "User is already a member of this chat."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateAsync(RemoveUserFromChatRequest requestObject)
        {
            var validationResult = await _removeUserFromChatValidator.ValidateAsync(requestObject);

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!await ChatIdExists(requestObject.Id))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.Id), "Chat Id does not exist."));
            }

            if (!await UserIdExists(requestObject.UserId))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.UserId), "UserId does not exist."));
            }

            if (!await IsUserAlreadyMemberAsync(requestObject.Id, requestObject.UserId))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.UserId), "The user is not a member of the chat"));
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

        private async Task<bool> IsUserAlreadyMemberAsync(int chatId, int userId)
        {
            return await _context.UserChat.AnyAsync(uc => uc.ChatId == chatId && uc.UserId == userId);
        }
    }
}
