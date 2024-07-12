using BusinessLogic.Requests.Message;
using FluentValidation;

namespace BusinessLogic.Validation.Validators.Message
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageRequest>
    {
        public CreateMessageValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text is required.")
                .MaximumLength(500)
                .WithMessage("Text cannot be longer than 500 characters.");

            RuleFor(x => x.ChatId)
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
