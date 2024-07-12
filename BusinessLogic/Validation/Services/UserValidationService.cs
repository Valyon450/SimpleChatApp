using BusinessLogic.Requests.User;
using BusinessLogic.Validation.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace BusinessLogic.Validation.Services
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IValidator<CreateUserRequest> _createValidator;
        private readonly IValidator<UpdateUserRequest> _updateValidator;

        public UserValidationService(
            IValidator<CreateUserRequest> createValidator,
            IValidator<UpdateUserRequest> updateValidator)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ValidationResult> ValidateAsync(CreateUserRequest requestObject)
        {
            var validationResult = await _createValidator.ValidateAsync(requestObject);

            return validationResult;
        }

        public async Task<ValidationResult> ValidateAsync(UpdateUserRequest requestObject)
        {
            var validationResult = await _updateValidator.ValidateAsync(requestObject);

            return validationResult;
        }
    }
}
