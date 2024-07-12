using BusinessLogic.Requests.Chat;
using FluentValidation;

namespace BusinessLogic.Validation.Validators.Chat
{
    public class AddUserToChatValidator : AbstractValidator<AddUserToChatRequest>
    {
        public AddUserToChatValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Chat Id is required.")
                .GreaterThan(0)
                .WithMessage("Chat Id must be a positive number.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User Id is required.")
                .GreaterThan(0)
                .WithMessage("User Id must be a positive number.");
        }
    }
}
