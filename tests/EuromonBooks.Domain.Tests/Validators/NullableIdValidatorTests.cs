using EuromonBooks.Domain.Validators;
using EuromonBooks.TestHelpers;
using FluentValidation.TestHelper;

namespace Mobecom.Mosaic.Domain.Tests.Validator
{
    public class NullableIdValidatorTests
    {
        public readonly NullableIdValidator _validator;

        public NullableIdValidatorTests()
        {
            _validator = new NullableIdValidator();
        }

        [FactAutoDisplayName]
        public void NullableIdValidator_HasChildValidators()
        {
            _validator.ShouldHaveChildValidator(x => x, typeof(IdValidator));
        }
    }
}