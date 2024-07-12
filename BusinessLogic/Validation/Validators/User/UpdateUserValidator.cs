﻿using BusinessLogic.Requests.User;
using FluentValidation;

namespace BusinessLogic.Validation.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName is required.")
                .MaximumLength(100)
                .WithMessage("UserName cannot be longer than 100 characters.");
        }
    }
}
