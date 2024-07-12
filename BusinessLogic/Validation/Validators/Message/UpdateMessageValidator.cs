using BusinessLogic.Requests.Message;
using FluentValidation;

namespace BusinessLogic.Validation.Validators.Message
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageRequest>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text is required.")
                .MaximumLength(500)
                .WithMessage("Text cannot be longer than 500 characters.");
        }
    }
}
