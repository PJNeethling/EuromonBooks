using EuromonBooks.Abstractions.Models;
using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.IsRequired);
        }
    }
}