using BusinessLogic.Requests.Chat;
using FluentValidation.Results;

namespace BusinessLogic.Validation.Services.Interfaces
{
    public interface IChatValidationService
    {
        Task<ValidationResult> ValidateAsync(CreateChatRequest requestObject);

        Task<ValidationResult> ValidateAsync(UpdateChatRequest requestObject);

        Task<ValidationResult> ValidateAsync(AddUserToChatRequest requestObject);

        Task<ValidationResult> ValidateAsync(RemoveUserFromChatRequest requestObject);
    }
}
