using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class GetAllUsersValidator : AbstractValidator<int?>
    {
        public GetAllUsersValidator()
        {
            RuleFor(x => x)
                .SetValidator(new NullableIdValidator(false, "StatusId"));
        }
    }
}