using EuromonBooks.Abstractions.Models;
using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class RegisterUserValidator : AbstractValidator<UserModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(ValidationMessages.IsRequired)
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").WithMessage(ValidationMessages.IsInvalid);
        }
    }
}