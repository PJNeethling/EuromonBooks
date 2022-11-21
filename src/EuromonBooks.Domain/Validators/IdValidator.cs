using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator(bool useIdAsPropertyName = true, string customPropertyName = null)
        {
            var usePropertyName = useIdAsPropertyName ? "Id" : customPropertyName ?? "";

            When(x => useIdAsPropertyName || customPropertyName != null, () =>
            {
                RuleFor(x => x)
                    .GreaterThan(0)
                    .WithMessage(ValidationMessages.IsInvalid)
                    .OverridePropertyName(usePropertyName);
            }).Otherwise(() =>
            {
                RuleFor(x => x)
                    .GreaterThan(0)
                    .WithMessage(ValidationMessages.IsInvalid);
            });
        }
    }
}