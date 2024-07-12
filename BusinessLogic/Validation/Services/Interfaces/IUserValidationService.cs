using BusinessLogic.Requests.User;
using FluentValidation.Results;

namespace BusinessLogic.Validation.Services.Interfaces
{
    public interface IUserValidationService
    {
        Task<ValidationResult> ValidateAsync(CreateUserRequest requestObject);

        Task<ValidationResult> ValidateAsync(UpdateUserRequest requestObject);
    }
}
