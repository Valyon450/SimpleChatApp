using BusinessLogic.Requests.User;
using BusinessLogic.Validation.Services.Interfaces;
using DataAccess.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Validation.Services
{
    public class UserValidationService : IUserValidationService
    {
        private readonly ISimpleChatDbContext _context;
        private readonly IValidator<CreateUserRequest> _createValidator;
        private readonly IValidator<UpdateUserRequest> _updateValidator;

        public UserValidationService(
            ISimpleChatDbContext context,
            IValidator<CreateUserRequest> createValidator,
            IValidator<UpdateUserRequest> updateValidator)
        {
            _context = context;
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

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (!await UserIdExists(requestObject.Id))
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(requestObject.Id), "User Id does not exist."));
            }

            return validationResult;
        }

        private async Task<bool> UserIdExists(int id)
        {
            return await _context.User.AnyAsync(e => e.Id == id);
        }
    }
}
