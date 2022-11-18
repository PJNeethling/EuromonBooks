using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator()
        {
            RuleFor(x => x)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.IsInvalid);
        }
    }
}