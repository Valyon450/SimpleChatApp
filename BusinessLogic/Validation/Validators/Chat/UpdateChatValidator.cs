using BusinessLogic.Requests.Chat;
using FluentValidation;

namespace BusinessLogic.Validation.Validators.Chat
{
    public class UpdateChatValidator : AbstractValidator<UpdateChatRequest>
    {
        public UpdateChatValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Chat name is required.")
                .MaximumLength(100)
                .WithMessage("Chat name cannot be longer than 100 characters.");
        }
    }
}
