﻿using EuromonBooks.Abstractions.Models;
using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.IsRequired);
        }
    }
}