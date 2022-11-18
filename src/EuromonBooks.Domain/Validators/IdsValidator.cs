using FluentValidation;

namespace EuromonBooks.Domain.Validators
{
    public class IdsValidator : AbstractValidator<List<int>>
    {
        public IdsValidator()
        {
            RuleForEach(x => x)
                .SetValidator(new IdValidator());
        }
    }
}