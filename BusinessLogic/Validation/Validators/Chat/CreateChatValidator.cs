using BusinessLogic.Requests.Chat;
using FluentValidation;

namespace BusinessLogic.Validation.Validators.Chat
{
    public class CreateChatValidator : AbstractValidator<CreateChatRequest>
    {
        public CreateChatValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Chat name is required.")
                .MaximumLength(100)
                .WithMessage("Chat name cannot be longer than 100 characters.");

            RuleFor(x => x.OwnerId)
                .NotEmpty()
                .WithMessage("Creator Id is required.")
                .GreaterThan(0)
                .WithMessage("Creator Id must be a positive number.");
        }
    }
}
