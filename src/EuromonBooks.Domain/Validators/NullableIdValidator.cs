using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class NullableIdValidator : AbstractValidator<int?>
    {
        public NullableIdValidator(bool useIdAsPropertyName = true, string customPropertyName = null)
        {
            When(x => x.HasValue, () =>
            {
                Transform(x => x, y => y.Value)
                    .SetValidator(new IdValidator(useIdAsPropertyName, customPropertyName));
            });
        }
    }
}