using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class UserUuidValidator : AbstractValidator<string>
    {
        public UserUuidValidator()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage(ValidationMessages.IsRequired);
        }
    }
}