using BusinessLogic.Requests.Message;
using FluentValidation.Results;

namespace BusinessLogic.Validation.Services.Interfaces
{
    public interface IMessageValidationService
    {
        Task<ValidationResult> ValidateAsync(CreateMessageRequest requestObject);

        Task<ValidationResult> ValidateAsync(UpdateMessageRequest requestObject);
    }
}
